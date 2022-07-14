namespace MyNumber.Service

open MyNumber.Service.UInt
open System.Text.RegularExpressions

module Int =
    let GetUIntNumber (number: string) =
        if number[0] = '-' then
            (-1, number[1..])
        else
            (1, number)

    let FormatInt (number: string) =
        let (sign, realNumber) = GetUIntNumber number
        let result = FormatUInt realNumber

        if result = "0" then "0"
        elif sign = 1 then result
        else "-" + result

    let IsInt (number: string) =
        let result = Regex.Match(number, "^[- | 0-9][0-9]*")
        result.Length = number.Length

    let IntCompare (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> FormatInt |> GetUIntNumber
        let (sign2, realNum2) = number2 |> FormatInt |> GetUIntNumber

        if sign1 < sign2 then
            -1
        elif sign1 > sign2 then
            1
        elif sign1 = 1 then
            UIntCompare realNum1 realNum2
        else
            -(UIntCompare realNum1 realNum2)

    let AddInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> FormatInt |> GetUIntNumber
        let (sign2, realNum2) = number2 |> FormatInt |> GetUIntNumber

        if sign1 = -1 && sign2 = -1 then
            "-" + (AddUInt realNum1 realNum2)
        elif sign1 = 1 && sign2 = 1 then
            AddUInt realNum1 realNum2
        else
            let compare = UIntCompare realNum1 realNum2

            if compare = 0 then
                "0"
            elif compare = -1 then
                if sign1 = -1 then
                    SubtractUInt realNum2 realNum1
                else
                    "-" + (SubtractUInt realNum2 realNum1)
            elif sign1 = -1 then
                "-" + (SubtractUInt realNum1 realNum2)
            else
                SubtractUInt realNum1 realNum2

    let SubtractInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> FormatInt |> GetUIntNumber
        let (sign2, realNum2) = number2 |> FormatInt |> GetUIntNumber

        if sign1 = -1 && sign2 = 1 then
            "-" + (AddUInt realNum1 realNum2)
        elif sign1 = 1 && sign2 = -1 then
            AddInt realNum1 realNum2
        else
            let compare = UIntCompare realNum1 realNum2

            if compare = 0 then
                "0"
            elif compare = 1 then
                if sign1 = 1 then
                    SubtractUInt realNum1 realNum2
                else
                    "-" + (SubtractUInt realNum1 realNum2)
            else if sign1 = 1 then
                "-" + (SubtractUInt realNum2 realNum1)
            else
                SubtractUInt realNum2 realNum1

    let MultiplyInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> FormatInt |> GetUIntNumber
        let (sign2, realNum2) = number2 |> FormatInt |> GetUIntNumber
        let rawResult = MultiplyUInt realNum1 realNum2

        if sign1 * sign2 > 0 then
            rawResult
        else
            "-" + rawResult

    let DivideInt (dividend: string) (divisor: string) =
        let (sign1, realDividend) = dividend |> FormatInt |> GetUIntNumber
        let (sign2, realDivisor) = divisor |> FormatInt |> GetUIntNumber
        let rawResult = DivideUInt realDividend realDivisor

        if sign1 * sign2 > 0 then
            rawResult
        else
            "-" + rawResult
