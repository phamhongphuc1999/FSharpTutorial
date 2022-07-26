namespace MyNumber.Number

open MyNumber.Number.IntNumber
open MyNumber.Number.UIntNumber
open MyNumber.Service.Fraction
open MyNumber.Error

module FractionNumber =
    type FractionNumber(numerator: string, denominator: string) =
        let mutable coreNumber = FormatFraction(numerator, denominator)

        do
            let (numerator, denominator) = coreNumber

            if denominator = "0" then
                raise (NotANumber("Not A Number"))

        new(numerator: IntNumber, denominator: UIntNumber) =
            FractionNumber(numerator.CoreNumber, denominator.CoreNumber)

        member this.Numerator = numerator
        member this.Denominator = denominator

        member this.CoreNumber
            with get () = coreNumber
            and set value = coreNumber <- FormatFraction value

        override this.ToString() =
            let (numerator, denominator) = coreNumber

            match (numerator, denominator) with
            | ("0", _) -> "0"
            | (_, "1") -> numerator
            | _ -> numerator + "/" + denominator

        member this.IsLessThan(number: FractionNumber) =
            let result = FractionCompare this.CoreNumber number.CoreNumber
            if result = -1 then true else false

        static member op_LessThan(number1: FractionNumber, number2: FractionNumber) = number1.IsLessThan(number2)

        member this.IsEqual(number: FractionNumber) =
            let result = FractionCompare this.CoreNumber (number.CoreNumber)
            if result = 0 then true else false

        static member op_Equality(number1: FractionNumber, number2: FractionNumber) = number1.IsEqual(number2)

        member this.IsNotEqual(number: FractionNumber) =
            let result = FractionCompare this.CoreNumber number.CoreNumber
            if result <> 0 then true else false

        static member op_Inequality(number1: FractionNumber, number2: FractionNumber) = number1.IsNotEqual(number2)

        member this.IsGresterThan(number: FractionNumber) =
            let result = FractionCompare this.CoreNumber (number.CoreNumber)
            if result = 1 then true else false

        static member op_GreaterThan(number1: FractionNumber, number2: FractionNumber) = number1.IsGresterThan(number2)

        static member IsNumber(numerator: string, denominator: string) = IsFraction(numerator, denominator)

        static member Parse(numerator: string, denominator: string) =
            (numerator, denominator)
            |> FormatFraction
            |> FractionNumber

        static member Compare(number1: FractionNumber, number2: FractionNumber) =
            FractionCompare number1.CoreNumber number2.CoreNumber

        member this.Add(number: FractionNumber) =
            (AddFraction this.CoreNumber number.CoreNumber)
            |> FractionNumber

        static member (+)(number1: FractionNumber, number2: FractionNumber) = number1.Add(number2)

        member this.Subtract(number: FractionNumber) =
            (SubtractFraction this.CoreNumber number.CoreNumber)
            |> FractionNumber

        static member (-)(number1: FractionNumber, number2: FractionNumber) = number1.Subtract(number2)

        member this.Multiple(number: FractionNumber) =
            (MultipleFraction this.CoreNumber number.CoreNumber)
            |> FractionNumber

        static member (*)(number1: FractionNumber, number2: FractionNumber) = number1.Multiple(number2)

        member this.Divide(number: FractionNumber) =
            (DivideFraction this.CoreNumber number.CoreNumber)
            |> FractionNumber

        static member (/)(number1: FractionNumber, number2: FractionNumber) = number1.Divide(number2)
