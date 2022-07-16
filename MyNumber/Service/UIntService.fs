namespace MyNumber.Service

open System.Text.RegularExpressions

module UInt =
    let FormatUInt (number: string) =
        let mutable check = true
        let len = number.Length
        let mutable count = 0

        while (count < len) && check do
            if number[count] = '0' then
                count <- count + 1
            else
                check <- false

        if not check then
            number[count..]
        else
            "0"

    let IsUInt (number: string) =
        let result = Regex.Match(number, "[0-9]*")
        result.Length = number.Length

    let UIntCompare (number1: string) (number2: string) =
        let fNum1 = FormatUInt number1
        let fNum2 = FormatUInt number2
        let len1 = fNum1.Length
        let len2 = fNum2.Length

        if len1 > len2 then
            1
        elif len1 < len2 then
            -1
        else
            let mutable check = true
            let mutable count = 0
            let mutable result = 0

            while (count < len1) && check do
                if fNum1[count] < fNum2[count] then
                    check <- false
                    result <- -1
                elif fNum1[count] > fNum2[count] then
                    check <- false
                    result <- 1
                else
                    count <- count + 1

            result

    let AddUInt (number1: string) (number2: string) =
        let fNum1 = FormatUInt number1
        let fNum2 = FormatUInt number2
        let len1 = fNum1.Length
        let len2 = fNum2.Length
        let mutable result = ""
        let mutable count = 1
        let mutable remain = 0

        while (count <= len1) && (count <= len2) do
            let n1 = int fNum1[len1 - count] - 48
            let n2 = int fNum2[len2 - count] - 48
            let temp = n1 + n2 + remain
            result <- (string (temp % 10)) + result
            remain <- temp / 10
            count <- count + 1

        while count <= len1 do
            let n1 = int fNum1[len1 - count] - 48
            let temp = n1 + remain
            result <- (string (temp % 10)) + result
            remain <- temp / 10
            count <- count + 1

        while count <= len2 do
            let n2 = int fNum2[len2 - count] - 48
            let temp = n2 + remain
            result <- (string (temp % 10)) + result
            remain <- temp / 10
            count <- count + 1

        if remain > 0 then
            result <- (string remain) + result

        result

    let SubtractUInt (number1: string) (number2: string) =
        let fNum1 = FormatUInt number1
        let fNum2 = FormatUInt number2
        let len1 = fNum1.Length
        let len2 = fNum2.Length
        let mutable result = ""
        let mutable count = 1
        let mutable remain = 0

        while (count <= len1) && (count <= len2) do
            let n1 = int fNum1[len1 - count] - 48
            let n2 = int fNum2[len2 - count] - 48

            if n1 >= n2 + remain then
                result <- (string (n1 - n2 - remain)) + result
                remain <- 0
            else
                result <- (string (n1 + 10 - n2 - remain)) + result
                remain <- 1

            count <- count + 1

        while count <= len1 do
            let n1 = int fNum1[len1 - count] - 48

            if n1 >= remain then
                result <- (string (n1 - remain)) + result
                remain <- 0
            else
                result <- (string (10 + n1 - remain)) + result
                remain <- 1

            count <- count + 1

        result

    let private SignerMultiply (number: string) (cNumber: char) =
        let mutable result = "0"
        let cNum = (int cNumber) - 48

        for count = 1 to cNum do
            result <- (AddUInt result number)

        result

    let MultiplyUInt (number1: string) (number2: string) =
        let fNum1 = FormatUInt number1
        let fNum2 = FormatUInt number2

        if fNum1 = "0" || fNum2 = "0" then
            "0"
        else
            let mutable result = ""

            for cNum1 in fNum1 do
                let temp = SignerMultiply fNum2 cNum1
                result <- result + "0"
                result <- (AddUInt temp result)

            result

    let DivideUInt (dividend: string) (divisor: string) =
        if divisor = "0" then
            raise (System.DivideByZeroException())
        elif divisor = "1" then
            dividend
        else
            let mutable result = ""
            let mutable remain = ""

            for cDividend in dividend do
                remain <- remain + (string cDividend)

                if remain = "0" then
                    result <- result + "0"
                    remain <- ""
                else
                    let remainCompare = UIntCompare remain divisor

                    if remainCompare = 0 then
                        result <- result + "1"
                        remain <- ""
                    elif remainCompare = -1 then
                        if result.Length > 0 then
                            result <- result + "0"
                    elif remainCompare = 1 then
                        let mutable count = 1
                        let mutable total = divisor
                        let mutable preTotal = ""
                        let mutable check = -1

                        while count <= 9 && check = -1 do
                            preTotal <- total
                            total <- (AddUInt total divisor)
                            count <- count + 1
                            check <- UIntCompare total remain

                        if count > 9 then
                            result <- result + "9"
                            remain <- (SubtractUInt remain preTotal)
                        elif check = 0 then
                            result <- result + (string count)
                            remain <- ""
                        else
                            result <- result + (string (count - 1))
                            remain <- (SubtractUInt remain preTotal)

            result

    let DivideModUInt (dividend: string) (divisor: string) =
        if divisor = "0" then
            raise (System.DivideByZeroException())
        elif divisor = "1" then
            "0"
        else
            let mutable result = "0"

            for cDividend in dividend do
                if result = "0" then
                    result <- (string cDividend)
                else
                    result <- result + (string cDividend)

                let resultCompare = UIntCompare result divisor

                if resultCompare = 0 then
                    result <- "0"
                elif resultCompare = 1 then
                    let mutable preTotal = divisor
                    let mutable total = AddUInt divisor divisor
                    let mutable check = UIntCompare total result

                    while check = -1 do
                        preTotal <- total
                        total <- AddUInt total divisor
                        check <- UIntCompare total result

                    if check = 0 then
                        result <- "0"
                    else
                        result <- SubtractUInt result preTotal

            FormatUInt result

    let RealDivideUInt (dividend: string) (divisor: string) =
        (DivideUInt dividend divisor, DivideModUInt dividend divisor)

    let MultiplyUInt10 (number1: string) (number2: string) =
        let mutable result = ""
        let mutable temp = "0"
        let mutable tNum2 = number2

        while UIntCompare tNum2 "0" = 1 do
            let (rInteger, rDecimal) = RealDivideUInt tNum2 "2"

            if rDecimal = "1" then
                result <- result + temp

            temp <- temp + temp
            tNum2 <- rInteger

        number1 + result

    let PowUInt (number1: string) (number2: string) =
        let mutable result = "1"
        let mutable tNum1 = number1
        let mutable tNum2 = number2

        while UIntCompare tNum2 "0" = 1 do
            let (rInteger, rDecimal) = RealDivideUInt tNum2 "2"

            if rDecimal = "1" then
                result <- MultiplyUInt result tNum1

            tNum1 <- MultiplyUInt tNum1 tNum1
            tNum2 <- rInteger

        result
