namespace MyNumber.Number

open MyNumber.Number.DecimalNumber
open MyNumber.Number.UIntNumber
open MyNumber.Number.IntNumber

module ComplexNumber =
    type ComplexNumber(realPart: DecimalNumber, imaginaryPart: DecimalNumber) =
        let mutable realPart = realPart
        let mutable imaginaryPart = imaginaryPart

        new(realPart: string, imaginaryPart: string) =
            ComplexNumber(DecimalNumber realPart, DecimalNumber imaginaryPart)

        member this.RealPart
            with get () = realPart
            and set value = realPart <- value

        member this.ImaginaryPart
            with get () = imaginaryPart
            and set value = imaginaryPart <- value

        member this.CoreNumber =
            let sReal = realPart.CoreNumber
            let mutable sImaginary = imaginaryPart.CoreNumber
            let mutable sign = "+"

            if (sImaginary[0] = '-') then
                sImaginary <- sImaginary[1..]
                sign <- "-"

            match (sReal, sImaginary) with
            | ("0", "0") -> "0"
            | ("0", _) ->
                match sImaginary with
                | "1" -> if sign = "+" then "i" else "-i"
                | _ ->
                    if sign = "+" then
                        sImaginary + "*i"
                    else
                        sign + sImaginary + "*i"
            | (_, "0") -> sReal
            | (_, "1") -> sReal + sign + "i"
            | _ -> sReal + sign + sImaginary + "*i"

        override this.ToString() = this.CoreNumber

        member this.ConjugateNumber() =
            (realPart, -imaginaryPart) |> ComplexNumber

        member this.IsEqual(number: ComplexNumber) =
            (this.RealPart = number.RealPart)
            && (this.ImaginaryPart = number.ImaginaryPart)

        static member op_Equality(number1: ComplexNumber, number2: ComplexNumber) = number1.IsEqual number2

        static member (~+)(number: ComplexNumber) = number

        static member (~-)(number: ComplexNumber) =
            (-number.RealPart, -number.ImaginaryPart)
            |> ComplexNumber

        member this.Add(number: ComplexNumber) =
            (this.RealPart + number.RealPart, this.ImaginaryPart + number.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: ComplexNumber, number2: ComplexNumber) = number1.Add number2

        static member (+)(number1: ComplexNumber, number2: UIntNumber) =
            (number1.RealPart + number2, number1.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: UIntNumber, number2: ComplexNumber) =
            (number1 + number2.RealPart, number2.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: ComplexNumber, number2: IntNumber) =
            (number1.RealPart + number2, number1.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: IntNumber, number2: ComplexNumber) =
            (number1 + number2.RealPart, number2.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: ComplexNumber, number2: DecimalNumber) =
            (number1.RealPart + number2, number1.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: DecimalNumber, number2: ComplexNumber) =
            (number1 + number2.RealPart, number2.ImaginaryPart)
            |> ComplexNumber

        member this.Subtract(number: ComplexNumber) =
            (this.RealPart - number.RealPart, this.ImaginaryPart - number.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: ComplexNumber, number2: ComplexNumber) = number1.Subtract number2

        static member (-)(number1: ComplexNumber, number2: UIntNumber) =
            (number1.RealPart - number2, number1.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: UIntNumber, number2: ComplexNumber) =
            (number1 - number2.RealPart, -number2.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: ComplexNumber, number2: IntNumber) =
            (number1.RealPart - number2, number1.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: IntNumber, number2: ComplexNumber) =
            (number1 - number2.RealPart, -number2.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: ComplexNumber, number2: DecimalNumber) =
            (number1.RealPart - number2, number1.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: DecimalNumber, number2: ComplexNumber) =
            (number1 - number2.RealPart, -number2.ImaginaryPart)
            |> ComplexNumber

        member this.Multiple(number: ComplexNumber) =
            ((this.RealPart * number.RealPart)
             - (this.ImaginaryPart * number.ImaginaryPart),
             (this.RealPart * number.ImaginaryPart)
             + (this.ImaginaryPart * number.RealPart))
            |> ComplexNumber

        static member (*)(number1: ComplexNumber, number2: ComplexNumber) = number1.Multiple number2

        static member (*)(number1: ComplexNumber, number2: UIntNumber) =
            (number1.RealPart * number2, number1.ImaginaryPart * number2)
            |> ComplexNumber

        static member (*)(number1: UIntNumber, number2: ComplexNumber) =
            (number2.RealPart * number1, number2.ImaginaryPart * number1)
            |> ComplexNumber

        static member (*)(number1: ComplexNumber, number2: IntNumber) =
            (number1.RealPart * number2, number1.ImaginaryPart * number2)
            |> ComplexNumber

        static member (*)(number1: IntNumber, number2: ComplexNumber) =
            (number2.RealPart * number1, number2.ImaginaryPart * number1)
            |> ComplexNumber

        static member (*)(number1: ComplexNumber, number2: DecimalNumber) =
            (number1.RealPart * number2, number1.ImaginaryPart * number2)
            |> ComplexNumber

        static member (*)(number1: DecimalNumber, number2: ComplexNumber) =
            (number2.RealPart * number1, number2.ImaginaryPart * number1)
            |> ComplexNumber

        member this.Divide(divisor: ComplexNumber) =
            let temp1 =
                (divisor.RealPart * divisor.RealPart)
                + (divisor.ImaginaryPart * divisor.ImaginaryPart)

            let xTemp =
                (this.RealPart * divisor.RealPart)
                + (this.ImaginaryPart * divisor.ImaginaryPart)

            let yTemp =
                (this.ImaginaryPart * divisor.RealPart)
                - (this.RealPart * divisor.ImaginaryPart)

            (xTemp / temp1, yTemp / temp1) |> ComplexNumber

        static member (/)(dividend: ComplexNumber, divisor: ComplexNumber) = dividend.Divide divisor

        static member (/)(dividend: ComplexNumber, divisor: UIntNumber) =
            (dividend.RealPart / divisor, dividend.ImaginaryPart / divisor)
            |> ComplexNumber

        static member (/)(dividend: UIntNumber, divisor: ComplexNumber) =
            let realDividend =
                (dividend.CoreNumber |> DecimalNumber, "0" |> DecimalNumber)
                |> ComplexNumber

            realDividend / divisor

        static member (/)(dividend: ComplexNumber, divisor: IntNumber) =
            (dividend.RealPart / divisor, dividend.ImaginaryPart / divisor)
            |> ComplexNumber

        static member (/)(dividend: IntNumber, divisor: ComplexNumber) =
            let realDividend =
                (dividend.CoreNumber |> DecimalNumber, "0" |> DecimalNumber)
                |> ComplexNumber

            realDividend / divisor

        static member (/)(dividend: ComplexNumber, divisor: DecimalNumber) =
            (dividend.RealPart / divisor, dividend.ImaginaryPart / divisor)
            |> ComplexNumber

        static member (/)(dividend: DecimalNumber, divisor: ComplexNumber) =
            let realDividend = (dividend, "0" |> DecimalNumber) |> ComplexNumber

            realDividend / divisor
