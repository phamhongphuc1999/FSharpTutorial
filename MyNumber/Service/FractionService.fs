namespace MyNumber.Service

open MyNumber.Service.UInt
open MyNumber.Service.Int

module Fraction =
    let CalculateGreatestCommonFactor (uintNum1: string) (uintNum2: string) =
        match (uintNum1, uintNum2) with
        | ("0", _)
        | ("1", _) -> uintNum1
        | (_, "0")
        | (_, "1") -> uintNum2
        | _ ->
            let mutable num1 = uintNum1
            let mutable num2 = uintNum2
            let check = UIntCompare num1 num2

            while check <> 0 do
                match check with
                | 1 -> num1 <- SubtractUInt num1 num2
                | _ -> num2 <- SubtractUInt num2 num1

            num1

    let FormatFraction (numerator: string, denominator: string) =
        let (sign, _denominator) = GetUIntNumber denominator
        let temp = CalculateGreatestCommonFactor numerator _denominator

        if temp = "1" then
            (numerator, denominator)
        else
            let newNumerator = DivideUInt numerator temp
            let newDenominator = DivideUInt _denominator temp

            match sign with
            | 1 -> (newNumerator, newDenominator)
            | _ -> (newNumerator, "-" + newDenominator)

    let AddFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator =
            ((MultiplyInt numerator1 denominator2), (MultiplyInt numerator2 denominator1))
            ||> AddInt

        let newDenominator = MultiplyInt denominator1 denominator2
        FormatFraction(newNumerator, newDenominator)

    let SubtractFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator =
            ((MultiplyInt numerator1 denominator2), (MultiplyInt numerator2 denominator1))
            ||> SubtractInt

        let newDenominator = MultiplyInt denominator1 denominator2
        FormatFraction(newNumerator, newDenominator)

    let MultipleFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator = MultiplyInt numerator1 numerator2
        let newDenominator = MultiplyInt denominator1 denominator2
        FormatFraction(newNumerator, newDenominator)

    let DivideFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator = MultiplyInt numerator1 denominator2
        let newDenominator = MultiplyInt denominator1 numerator2
        FormatFraction(newNumerator, newDenominator)
