namespace MyNumber.Service

open MyNumber.Service.UInt
open System.Text.RegularExpressions

module Decimal =
    let GetIntegerAndDecimal (number: string) =
        let mutable sign = 1
        let mutable realNum = number

        if realNum[0] = '-' then
            sign <- -1
            realNum <- realNum[1..]
        printfn "%s" realNum

        if realNum[0] = '.' then
            (sign, "0", realNum[1..])
        else
            let len = realNum.Length

            if realNum[len - 1] = '.' then
                (sign, realNum[0 .. (len - 2)], "")
            else
                let parts = realNum.Split([| '.' |])
                (sign, parts[0], parts[1])

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

    let IsDecimal (number: string) =
        let result1 = Regex.Match(number, "[0-9]*.[0-9]*")

        if result1.Length = number.Length then
            true
        else
            let result2 = Regex.Match(number, "-[0-9]*.[0-9]*")
            result2.Length = number.Length

    let DecimalCompare (number1: string) (number2: string) = ()

    let AddDecimal (number1: string) (number2: string) = ()

    let SubtractDecimal (number1: string) (number2: string) = ()

    let MultiplyDecimal (number1: string) (number2: string) = ()

    let DivideDecimal (number1: string) (number2: string) = ()
