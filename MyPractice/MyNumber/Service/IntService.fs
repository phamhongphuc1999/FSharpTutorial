namespace MyNumber.Service

open MyNumber.Service.UInt
open System.Text.RegularExpressions
open MyNumber.Error

module Int =
    let IsInt (number: string) =
        let result = Regex.Match(number, "^[- | 0-9][0-9]*")
        result.Length = number.Length

    let GetUIntNumber (number: string) =
        if number.[0] = '-' then
            (-1, number.[1..])
        else
            (1, number)

    let FormatInt (number: string) =
        if not (IsInt number) then
            raise (NotANumber("Not A Number"))

        let (sign, realNumber) = GetUIntNumber number
        let result = FormatUInt realNumber

        if result = "0" then "0"
        elif sign = 1 then result
        else "-" + result

    let DeepGetIntNumber = FormatInt >> GetUIntNumber

    let IntCompare (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> DeepGetIntNumber
        let (sign2, realNum2) = number2 |> DeepGetIntNumber

        if sign1 < sign2 then
            -1
        elif sign1 > sign2 then
            1
        elif sign1 = 1 then
            UIntCompare realNum1 realNum2
        else
            -(UIntCompare realNum1 realNum2)

    let AddInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> DeepGetIntNumber
        let (sign2, realNum2) = number2 |> DeepGetIntNumber

        match (sign1, sign2) with
        | (-1, -1) -> "-" + (AddUInt realNum1 realNum2)
        | (1, 1) -> AddUInt realNum1 realNum2
        | _ ->
            let compare = UIntCompare realNum1 realNum2

            match (compare, sign1) with
            | (0, _) -> "0"
            | (-1, -1) -> SubtractUInt realNum2 realNum1
            | (-1, 1) -> "-" + (SubtractUInt realNum2 realNum1)
            | (1, -1) -> "-" + (SubtractUInt realNum1 realNum2)
            | _ -> SubtractUInt realNum1 realNum2

    let SubtractInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> DeepGetIntNumber
        let (sign2, realNum2) = number2 |> DeepGetIntNumber

        match (sign1, sign2) with
        | (-1, 1) -> "-" + (AddUInt realNum1 realNum2)
        | (1, -1) -> AddInt realNum1 realNum2
        | _ ->
            let compare = UIntCompare realNum1 realNum2

            match (compare, sign1) with
            | (0, _) -> "0"
            | (1, 1) -> SubtractUInt realNum1 realNum2
            | (1, -1) -> "-" + (SubtractUInt realNum1 realNum2)
            | (-1, 1) -> "-" + (SubtractUInt realNum2 realNum1)
            | _ -> SubtractUInt realNum2 realNum1

    let MultiplyInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> DeepGetIntNumber
        let (sign2, realNum2) = number2 |> DeepGetIntNumber
        let result = MultiplyUInt realNum1 realNum2

        if sign1 * sign2 > 0 then
            result
        else
            "-" + result

    let DivideInt (dividend: string) (divisor: string) =
        let (sign1, realDividend) = dividend |> DeepGetIntNumber
        let (sign2, realDivisor) = divisor |> DeepGetIntNumber
        let result = DivideUInt realDividend realDivisor

        if sign1 * sign2 > 0 then
            result
        else
            "-" + result

    let MultiplyInt10 (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> DeepGetIntNumber
        let result = MultiplyUInt10 realNum1 number2

        if sign1 = -1 then
            "-" + result
        else
            result

    let PowInt (number1: string) (number2: string) =
        let (sign1, realNum1) = number1 |> DeepGetIntNumber
        let result = PowUInt realNum1 number2
        let modNum2 = DivideModUInt number2 "2"

        if sign1 = -1 && modNum2 = "1" then
            "-" + result
        else
            result
