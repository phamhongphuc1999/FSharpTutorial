namespace MyNumber.Service

open MyNumber.Service.UInt
open MyNumber.Service.Int

module Fraction =
    let IsFraction (numerator: string, denominator: string) =
        (IsInt numerator) && (IsUInt denominator)

    let FormatFraction (numerator: string, denominator: string) =
        let (sign, _numerator) = GetUIntNumber numerator
        let temp = CalculateGreatestCommonFactor _numerator denominator

        if temp = "1" then
            (numerator, denominator)
        else
            let newNumerator = DivideUInt _numerator temp
            let newDenominator = DivideUInt denominator temp

            match sign with
            | 1 -> (newNumerator, newDenominator)
            | _ -> ("-" + newNumerator, newDenominator)

    let FractionCompare (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let num1 = MultipliedInt numerator1 denominator2
        let num2 = MultipliedInt numerator2 denominator1
        IntCompare num1 num2

    let AddFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator =
            ((MultipliedInt numerator1 denominator2), (MultipliedInt numerator2 denominator1))
            ||> AddInt

        let newDenominator = MultipliedInt denominator1 denominator2
        FormatFraction(newNumerator, newDenominator)

    let SubtractFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator =
            ((MultipliedInt numerator1 denominator2), (MultipliedInt numerator2 denominator1))
            ||> SubtractInt

        let newDenominator = MultipliedInt denominator1 denominator2
        FormatFraction(newNumerator, newDenominator)

    let MultipliedFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator = MultipliedInt numerator1 numerator2
        let newDenominator = MultipliedInt denominator1 denominator2
        FormatFraction(newNumerator, newDenominator)

    let DivideFraction (numerator1: string, denominator1: string) (numerator2: string, denominator2: string) =
        let newNumerator = MultipliedInt numerator1 denominator2
        let newDenominator = MultipliedInt denominator1 numerator2
        FormatFraction(newNumerator, newDenominator)
