namespace MyNumber.Number

open MyNumber.Number.DecimalNumber

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

        member this.Add(number: ComplexNumber) =
            (this.RealPart + number.RealPart, this.ImaginaryPart + number.ImaginaryPart)
            |> ComplexNumber

        static member (+)(number1: ComplexNumber, number2: ComplexNumber) = number1.Add number2

        member this.Subtract(number: ComplexNumber) =
            (this.RealPart - number.RealPart, this.ImaginaryPart - number.ImaginaryPart)
            |> ComplexNumber

        static member (-)(number1: ComplexNumber, number2: ComplexNumber) = number1.Subtract number2

        member this.Multiple(number: ComplexNumber) =
            ((this.RealPart * number.RealPart)
             - (this.ImaginaryPart * number.ImaginaryPart),
             (this.RealPart * number.ImaginaryPart)
             + (this.ImaginaryPart * number.RealPart))
            |> ComplexNumber

        static member (*)(number1: ComplexNumber, number2: ComplexNumber) = number1.Multiple number2

        member this.Divide(number: ComplexNumber) =
            let temp1 =
                (number.RealPart * number.RealPart)
                + (number.ImaginaryPart * number.ImaginaryPart)

            let xTemp =
                (this.RealPart * number.RealPart)
                + (this.ImaginaryPart * number.ImaginaryPart)

            let yTemp =
                (this.ImaginaryPart * number.RealPart)
                - (this.RealPart * number.ImaginaryPart)

            (xTemp / temp1, yTemp / temp1) |> ComplexNumber

        static member (/)(number1: ComplexNumber, number2: ComplexNumber) = number1.Divide number2
