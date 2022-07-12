namespace MyNumber.Service

module Integer =
    let FormatNumber (number: string) = ()

    let IsUInt (number: string) =
        let mutable check = true
        let len = number.Length
        let mutable count = 0

        while (count < len) && check do
            let unitC = int number[count]

            if 48 > unitC || 57 < unitC then
                check <- false
            else
                count <- count + 1

        check

    let IsInt (number: string) =
        let firstN = number[0]

        if firstN <> '-'
           || 48 > int firstN
           || 57 < int firstN then
            false
        else
            let len = number.Length
            IsUInt number[1 .. (len - 1)]


    let AddIntegerNumber (number1: string) (number2: string) =
        let len1 = number1.Length
        let len2 = number2.Length
        let mutable result = ""
        let mutable count = 1
        let mutable remain = 0

        while (count <= len1) && (count <= len2) do
            let n1 = int number1[len1 - count] - 48
            let n2 = int number2[len2 - count] - 48
            let temp = n1 + n2 + remain
            result <- (string (temp % 10)) + result
            remain <- temp / 10
            count <- count + 1

        while count <= len1 do
            let n1 = int number1[len1 - count] - 48
            let temp = n1 + remain
            result <- (string (temp % 10)) + result
            remain <- temp / 10
            count <- count + 1

        while count <= len2 do
            let n2 = int number2[len2 - count] - 48
            let temp = n2 + remain
            result <- (string (temp % 10)) + result
            remain <- temp / 10
            count <- count + 1

        if remain > 0 then
            result <- (string remain) + result

        result

    let SubtractIntegerNumber (number1: string) (number2: string) = ()
