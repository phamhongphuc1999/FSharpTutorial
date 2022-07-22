namespace MyNumber.Number

open MyNumber.Number.IntNumber
open MyNumber.Number.UIntNumber
open MyNumber.Error

module Fraction =
    type Fraction(numerator: IntNumber, denominator: UIntNumber) =
        let mutable numerator = numerator
        let mutable denominator = denominator

        do
            if denominator.CoreNumber = "0" then
                raise (NotANumber("Not A Number"))

        new(numerator: string, denominator: string) = Fraction(IntNumber numerator, UIntNumber denominator)

        member this.Numerator
            with get () = numerator
            and set value = numerator <- value

        member this.Denominator
            with get () = denominator
            and set value =
                denominator <- value

                if denominator.CoreNumber = "0" then
                    raise (NotANumber("Not A Number"))

        member this.CoreNumber =
            match (numerator.CoreNumber, denominator.CoreNumber) with
            | ("0", _) -> "0"
            | (_, "1") -> numerator.CoreNumber
            | _ ->
                numerator.CoreNumber
                + "/"
                + denominator.CoreNumber

        override this.ToString() = this.CoreNumber
