namespace MyNumber.Number

open System
open MyNumber.Number.IntNumber
open MyNumber.Number.UIntNumber
open MyNumber.Service.Decimal
open MyNumber.Error

module DecimalNumber =
    type DecimalNumber(coreNumber: string) =
        let mutable coreNumber = FormatDecimal coreNumber

        do
            if not (IsDecimal coreNumber) then
                raise (NotANumber("Not A Number"))

        member this.CoreNumber
            with get () = coreNumber
            and set value = coreNumber <- FormatDecimal value

        override this.ToString() = this.CoreNumber

        interface IComparable<DecimalNumber> with
            member this.CompareTo obj =
                let num1 = this.CoreNumber
                let num2 = obj.CoreNumber
                DecimalCompare num1 num2

        interface IComparable with
            member this.CompareTo obj =
                match obj with
                | null -> 1
                | :? DecimalNumber as other -> (this :> IComparable<_>).CompareTo other
                | _ -> invalidArg "obj" "not a Category"

        interface IEquatable<DecimalNumber> with
            member this.Equals obj = this.IsEqual(obj)

        override this.Equals obj =
            match obj with
            | :? DecimalNumber as other -> (this :> IEquatable<_>).Equals other
            | _ -> false

        override this.GetHashCode() = this.GetHashCode()

        member this.Ceiling(exponent: int) =
            let temp = DecimalCeiling coreNumber exponent
            temp |> DecimalNumber

        member this.IsLessThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.CoreNumber)
            if result = -1 then true else false

        static member op_LessThan(number1: DecimalNumber, number2: DecimalNumber) = number1.IsLessThan(number2)

        member this.IsEqual(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.CoreNumber)
            if result = 0 then true else false

        static member op_Equality(number1: DecimalNumber, number2: DecimalNumber) = number1.IsEqual(number2)

        member this.IsNotEqual(number: DecimalNumber) =
            let result = DecimalCompare this.CoreNumber number.CoreNumber
            if result <> 0 then true else false

        static member op_Inequality(number1: DecimalNumber, number2: DecimalNumber) = number1.IsNotEqual(number2)

        member this.IsGresterThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.CoreNumber)
            if result = 1 then true else false

        static member op_GreaterThan(number1: DecimalNumber, number2: DecimalNumber) = number1.IsGresterThan(number2)

        member this.GetIntegerAndDecimal() =
            let (sign, integerPart, decimalPart) = GetIntegerAndDecimal this.CoreNumber
            (sign, integerPart |> UIntNumber, decimalPart |> UIntNumber)

        static member IsNumber(number: string) = IsDecimal number

        static member Parst(coreNumber: string) = DecimalNumber(coreNumber)

        static member Compare (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber
            DecimalCompare sNumber1 sNumber2

        static member (~+)(number: DecimalNumber) = number

        static member (~-)(number: DecimalNumber) =
            let sNumber = number.CoreNumber

            match sNumber[0] with
            | '-' -> sNumber[1..] |> DecimalNumber
            | _ -> ("-" + sNumber) |> DecimalNumber

        member this.Add(number: DecimalNumber) =
            (AddDecimal this.CoreNumber number.CoreNumber)
            |> DecimalNumber

        static member (+)(number1: DecimalNumber, number2: DecimalNumber) = number1.Add number2

        static member (+=)(number1: DecimalNumber, number2: DecimalNumber) =
            number1.CoreNumber <- AddDecimal number1.CoreNumber number2.CoreNumber

        static member (+)(number1: DecimalNumber, number2: UIntNumber) =
            (AddDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (+)(number1: UIntNumber, number2: DecimalNumber) =
            (AddDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (+)(number1: DecimalNumber, number2: IntNumber) =
            (AddDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (+)(number1: IntNumber, number2: DecimalNumber) =
            (AddDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        member this.Subtract(number: DecimalNumber) =
            (SubtractDecimal this.CoreNumber number.CoreNumber)
            |> DecimalNumber

        static member (-)(number1: DecimalNumber, number2: DecimalNumber) = number1.Subtract number2

        static member (-=)(number1: DecimalNumber, number2: DecimalNumber) =
            number1.CoreNumber <- SubtractDecimal number1.CoreNumber number2.CoreNumber

        static member (-)(number1: DecimalNumber, number2: UIntNumber) =
            (SubtractDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (-)(number1: UIntNumber, number2: DecimalNumber) =
            (SubtractDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (-)(number1: DecimalNumber, number2: IntNumber) =
            (SubtractDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (-)(number1: IntNumber, number2: DecimalNumber) =
            (SubtractDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        member this.Multiply10(number: UIntNumber) =
            (MultiplyDecimal10 this.CoreNumber number.CoreNumber)
            |> DecimalNumber

        static member (.*)(number1: DecimalNumber, number2: UIntNumber) = number1.Multiply10 number2

        member this.Multiply(number: DecimalNumber) =
            (MultiplyDecimal this.CoreNumber number.CoreNumber)
            |> DecimalNumber

        static member (*)(number1: DecimalNumber, number2: DecimalNumber) = number1.Multiply number2

        static member op_MultiplyAssignment(number1: DecimalNumber, number2: DecimalNumber) =
            number1.CoreNumber = MultiplyDecimal number1.CoreNumber number2.CoreNumber

        static member (*)(number1: DecimalNumber, number2: UIntNumber) =
            (MultiplyDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (*)(number1: UIntNumber, number2: DecimalNumber) =
            (MultiplyDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (*)(number1: DecimalNumber, number2: IntNumber) =
            (MultiplyDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        static member (*)(number1: IntNumber, number2: DecimalNumber) =
            (MultiplyDecimal number1.CoreNumber number2.CoreNumber)
            |> DecimalNumber

        member this.Divide10(number: UIntNumber) =
            (MultiplyDecimal10 this.CoreNumber ("-" + number.CoreNumber))
            |> DecimalNumber

        static member (./)(number1: DecimalNumber, number2: UIntNumber) = number1.Divide10 number2

        member this.Divide (divisor: DecimalNumber) (accuracy: int) =
            (DivideDecimal this.CoreNumber divisor.CoreNumber accuracy)
            |> DecimalNumber

        static member (/)(dividend: DecimalNumber, divisor: DecimalNumber) = dividend.Divide divisor 10

        static member (/=)(number1: DecimalNumber, number2: DecimalNumber) =
            number1.CoreNumber = DivideDecimal number1.CoreNumber number2.CoreNumber 10

        static member (/)(dividend: DecimalNumber, divisor: UIntNumber) =
            (DivideDecimal dividend.CoreNumber divisor.CoreNumber 10)
            |> DecimalNumber

        static member (/)(dividend: UIntNumber, divisor: DecimalNumber) =
            (DivideDecimal dividend.CoreNumber divisor.CoreNumber 10)
            |> DecimalNumber

        static member (/)(dividend: DecimalNumber, divisor: IntNumber) =
            (DivideDecimal dividend.CoreNumber divisor.CoreNumber 10)
            |> DecimalNumber

        static member (/)(dividend: IntNumber, divisor: DecimalNumber) =
            (dividend.CoreNumber, divisor.CoreNumber, 10)
            |||> DivideDecimal
            |> DecimalNumber

        member this.Pow(number: UIntNumber) =
            (this.CoreNumber, number.CoreNumber)
            ||> PowDecimal
            |> DecimalNumber

        static member (.^)(number1: DecimalNumber, number2: UIntNumber) = number1.Pow number2

        member this.Pow(number: IntNumber) =
            let (sign, num) = number.GetUInt()
            let result = (this.CoreNumber, num.CoreNumber) ||> PowDecimal

            match sign with
            | 1 -> result |> DecimalNumber
            | _ ->
                ("1", result, 10)
                |||> DivideDecimal
                |> DecimalNumber

        static member (.^)(number1: DecimalNumber, number2: IntNumber) = number1.Pow number2
