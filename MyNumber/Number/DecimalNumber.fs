namespace MyNumber.Number

open System
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
            and set value = coreNumber <- value

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
            | :? UIntNumber as other -> (this :> IEquatable<_>).Equals other
            | _ -> false

        override this.GetHashCode() = this.GetHashCode()

        member this.Ceiling(exponent: int) =
            let temp = DecimalCeiling coreNumber exponent
            this.CoreNumber <- temp

        static member StaticCeiling (number: DecimalNumber) (exponent: int) =
            let sNum = number.CoreNumber
            (DecimalCeiling sNum exponent) |> DecimalNumber

        member this.IsLessThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.CoreNumber)
            if result = -1 then true else false

        static member op_LessThan(number1: DecimalNumber, number2: DecimalNumber) = number1.IsLessThan(number2)

        member this.IsEqual(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.CoreNumber)
            if result = 0 then true else false

        static member op_Equality(number1: DecimalNumber, number2: DecimalNumber) = number1.IsEqual(number2)

        member this.IsGresterThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.CoreNumber)
            if result = 1 then true else false

        static member op_GreaterThan(number1: DecimalNumber, number2: DecimalNumber) = number1.IsGresterThan(number2)

        static member IsDecimal(number: string) = IsDecimal number

        static member Parst(coreNumber: string) = DecimalNumber(coreNumber)

        static member Compare (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber
            DecimalCompare sNumber1 sNumber2

        static member (~-)(number: DecimalNumber) =
            let sNumber = number.CoreNumber

            match sNumber[0] with
            | '-' -> sNumber[1..] |> DecimalNumber
            | _ -> ("-" + sNumber) |> DecimalNumber

        static member Add (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber
            (AddDecimal sNumber1 sNumber2) |> DecimalNumber

        static member (+)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Add number1 number2

        static member Subtract (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber

            (SubtractDecimal sNumber1 sNumber2)
            |> DecimalNumber

        static member (-)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Subtract number1 number2

        static member Multiply10 (number1: DecimalNumber) (number2: UIntNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber

            (MultiplyDecimal10 sNumber1 sNumber2)
            |> DecimalNumber

        static member (.*)(number1: DecimalNumber, number2: UIntNumber) =
            DecimalNumber.Multiply10 number1 number2

        static member Multiply (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber

            (MultiplyDecimal sNumber1 sNumber2)
            |> DecimalNumber

        static member (*)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Multiply number1 number2

        static member Divide10 (number1: DecimalNumber) (number2: UIntNumber) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber

            (MultiplyDecimal10 sNumber1 ("-" + sNumber2))
            |> DecimalNumber

        static member (./)(number1: DecimalNumber, number2: UIntNumber) = DecimalNumber.Divide10 number1 number2

        static member Divide (number1: DecimalNumber) (number2: DecimalNumber) (accuracy: int) =
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber

            (DivideDecimal sNumber1 sNumber2 accuracy)
            |> DecimalNumber

        static member (/)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Divide number1 number2 10
