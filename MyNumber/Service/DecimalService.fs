namespace MyNumber.Service

open MyNumber.Service.UInt
open MyNumber.Service.Int
open System.Text.RegularExpressions

type Mode =
    | normal = 1
    | multiply = 2
    | divide = 3

module Decimal =
    let GetIntegerAndDecimal (number: string) =
        let mutable sign = 1
        let mutable realNum = number

        if realNum[0] = '-' then
            sign <- -1
            realNum <- realNum[1..]

        if realNum[0] = '.' then
            (sign, "0", realNum[1..])
        else
            let len = realNum.Length

            if realNum[len - 1] = '.' then
                (sign, realNum[0 .. (len - 2)], "")
            else
                let parts = realNum.Split([| '.' |])

                if parts.Length > 1 then
                    (sign, parts[0], parts[1])
                else
                    (sign, parts[0], "")

    let FormatDecimal (number: string) =
        let (sign, intNumber, decimalNumber) = GetIntegerAndDecimal number
        let intNumberFormat = FormatUInt intNumber
        let mutable count = 1
        let len = decimalNumber.Length
        let mutable check = true
        let mutable decimalNumberFormat = ""

        while (count <= len) && check do
            if decimalNumber[len - count] = '0' then
                count <- count + 1
            else
                check <- false

        if not check then
            decimalNumberFormat <- decimalNumber[0 .. (len - count)]
        else
            decimalNumberFormat <- ""

        let mutable result = ""

        if decimalNumberFormat = "" then
            result <- intNumberFormat
        else
            result <- intNumberFormat + "." + decimalNumberFormat

        if sign = -1 then result <- "-" + result
        result

    let DeepGetIntegerAndDecimal = FormatDecimal >> GetIntegerAndDecimal

    let IsDecimal (number: string) =
        let result1 = Regex.Match(number, "[0-9]*.[0-9]*")

        if result1.Length = number.Length then
            true
        else
            let result2 = Regex.Match(number, "-[0-9]*.[0-9]*")
            result2.Length = number.Length

    let DecimalCeiling (number: string) (exponent: int) =
        let (sign, integerNum, decimalNum) = DeepGetIntegerAndDecimal number
        let len = decimalNum.Length

        if len <= exponent then
            number
        else
            let mutable exDecimalNum = decimalNum[0..exponent]
            let cNum = (decimalNum[exponent] |> int) - 48
            let realUp = (sign = 1 && cNum >= 5) || (sign = -1 && cNum < 5)

            match realUp with
            | true -> exDecimalNum <- AddUInt exDecimalNum "1"
            | false -> exDecimalNum <- SubtractUInt exDecimalNum "1"

            if sign = 1 then
                integerNum + "." + exDecimalNum
            else
                "-" + integerNum + "." + exDecimalNum

    let DecimalCompare (number1: string) (number2: string) =
        let (sign1, intNumber1, decimalNumber1) = number1 |> DeepGetIntegerAndDecimal

        let (sign2, intNumber2, decimalNumber2) = number2 |> DeepGetIntegerAndDecimal

        if sign1 = -1 && sign2 = 1 then
            -1
        elif sign1 = 1 && sign2 = -1 then
            1
        else
            let temp = UIntCompare intNumber1 intNumber2

            if temp <> 0 then
                sign1 * temp
            else
                let len1 = decimalNumber1.Length
                let len2 = decimalNumber2.Length
                let mutable count = 0
                let mutable check = true
                let mutable result = 1

                while count < len1 && count < len2 && check do
                    if decimalNumber1[count] < decimalNumber2[count] then
                        result <- -1
                        check <- false
                    elif decimalNumber1[count] > decimalNumber2[count] then
                        check <- false

                    count <- count + 1

                if not check then sign1 * result
                elif count < len1 then sign1
                elif count < len2 then -sign1
                else 0

    let private MultiplyUIntDecimal10 (number: string) (uintNum: string) =
        let (sign, intNumber, decimalNumber) = number |> DeepGetIntegerAndDecimal
        let mutable result = ""

        if decimalNumber = "0" then
            result <- MultiplyUInt10 number uintNum
        else
            let lenDecimal = decimalNumber.Length |> string
            let decimalCompare = UIntCompare lenDecimal uintNum

            if decimalCompare = 0 then
                result <- intNumber + decimalNumber
            elif decimalCompare = 1 then
                let index = (uintNum |> int) - 1
                let integerPart = intNumber + decimalNumber[0..index]
                let decimalPart = decimalNumber[(index + 1) ..]
                result <- integerPart + "." + decimalPart
            else
                let temp = MultiplyUInt10 decimalNumber (SubtractUInt uintNum lenDecimal)
                result <- intNumber + temp

        if sign = 1 then
            FormatDecimal result
        else
            "-" + FormatDecimal result

    let private DivideUInt10 (number: string) (uintNum: string) =
        let lenInt = number.Length
        let len = lenInt |> string
        let lenCompare = UIntCompare len uintNum

        if lenCompare = 0 then
            "0." + number
        elif lenCompare = 1 then
            let index = (uintNum |> int)
            let integerPart = number[0 .. (lenInt - index - 1)]
            let decimalPart = number[(lenInt - index) ..]
            integerPart + "." + decimalPart
        else
            let mutable result = ""
            let mutable temp = "0"
            let mutable tNum2 = SubtractUInt uintNum len

            while UIntCompare tNum2 "0" = 1 do
                let (rInteger, rDecimal) = RealDivideUInt tNum2 "2"

                if rDecimal = "1" then
                    result <- result + temp

                temp <- temp + temp
                tNum2 <- rInteger

            "0." + result + number

    let private DivideUIntDecimal10 (number: string) (uintNum: string) =
        let (sign, intNumber, decimalNumber) = number |> DeepGetIntegerAndDecimal
        let temp = DivideUInt10 intNumber uintNum
        let result = temp + decimalNumber

        if sign = 1 then
            FormatDecimal result
        else
            "-" + FormatDecimal result

    let MultiplyDecimal10 (number: string) (intNum: string) =
        let mutable sign = ""
        let mutable num = number

        if number[0] = '-' then
            sign <- "-"
            num <- number[1..]

        if intNum[0] <> '-' then
            sign + MultiplyUIntDecimal10 num intNum
        else
            sign + DivideUIntDecimal10 num intNum[1..]

    let private TransformInt (number1: string) (number2: string) (mode: Mode) =
        let (sign1, intNumber1, decimalNumber1) = number1 |> DeepGetIntegerAndDecimal
        let (sign2, intNumber2, decimalNumber2) = number2 |> DeepGetIntegerAndDecimal

        let decimalLen1 = decimalNumber1.Length |> string
        let decimalLen2 = decimalNumber2.Length |> string
        let mutable tempNum1 = ""
        let mutable tempNum2 = ""
        let decimalCompare = UIntCompare decimalLen1 decimalLen2
        let mutable smallLen = decimalLen1
        let mutable bigLen = decimalLen2

        if decimalCompare >= 0 then
            tempNum1 <- MultiplyUIntDecimal10 number1 decimalLen1
            tempNum2 <- MultiplyDecimal10 number2 decimalLen1
        else
            tempNum1 <- MultiplyUIntDecimal10 number1 decimalLen2
            tempNum2 <- MultiplyDecimal10 number2 decimalLen2
            smallLen <- decimalLen2
            bigLen <- decimalLen1

        match mode with
        | Mode.normal -> (tempNum1, tempNum2, bigLen)
        | Mode.multiply -> (tempNum1, tempNum2, AddInt decimalLen1 decimalLen2)
        | _ -> (tempNum1, tempNum2, SubtractUInt bigLen smallLen)

    let AddDecimal (number1: string) (number2: string) =
        let (tempNum1, tempNum2, len) = TransformInt number1 number2 Mode.normal

        DivideUIntDecimal10 (AddInt tempNum1 tempNum2) len

    let SubtractDecimal (number1: string) (number2: string) =
        let (tempNum1, tempNum2, len) = TransformInt number1 number2 Mode.normal
        DivideUIntDecimal10 (AddInt tempNum1 tempNum2) len

    let MultiplyDecimal (number1: string) (number2: string) =
        let (tempNum1, tempNum2, len) = TransformInt number1 number2 Mode.multiply
        DivideUIntDecimal10 (MultiplyInt tempNum1 tempNum2) len

    let DivideDecimal (number1: string) (number2: string) (accuracy: int) =
        let (tempNum1, tempNum2, len) = TransformInt number1 number2 Mode.divide
        let (sign1, tNum1) = GetUIntNumber tempNum1
        let (sign2, tNum2) = GetUIntNumber tempNum2
        let (rawResult, reamin) = RealDivideUInt tNum1 tNum2
        let mutable remainResult = ""
        let mutable rRemain = reamin
        let len2 = tNum2.Length
        let mutable rAccuracy = 0

        while rAccuracy < accuracy && rRemain <> "0" do
            rRemain <- MultiplyUInt10 rRemain (string len2)
            let mutable (temp1, temp2) = RealDivideUInt rRemain tNum2

            while temp1.Length < len2 do
                temp1 <- "0" + temp1

            remainResult <- remainResult + temp1
            rAccuracy <- remainResult.Length
            rRemain <- temp2

        rawResult + "." + remainResult
