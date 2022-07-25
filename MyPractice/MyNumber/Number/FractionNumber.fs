namespace MyNumber.Number

open MyNumber.Number.IntNumber
open MyNumber.Number.UIntNumber
open MyNumber.Service.Fraction
open MyNumber.Error

module FractionNumber =
    type FractionNumber(numerator: string, denominator: string) =
        let mutable coreNumber = (numerator, denominator)

        do
            let (numerator, denominator) = coreNumber

            if denominator = "0" then
                raise (NotANumber("Not A Number"))

        new(numerator: IntNumber, denominator: UIntNumber) =
            FractionNumber(numerator.CoreNumber, denominator.CoreNumber)

        member this.CoreNumber
            with get () = coreNumber
            and set value = coreNumber <- FormatFraction value

        override this.ToString() =
            let (numerator, denominator) = coreNumber

            match (numerator, denominator) with
            | ("0", _) -> "0"
            | (_, "1") -> numerator
            | _ -> numerator + "/" + denominator

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
