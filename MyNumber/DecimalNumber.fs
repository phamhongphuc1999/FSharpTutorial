namespace MyNumber

open System
open MyNumber.UIntNumber
open MyNumber.Service.Decimal
open MyNumber.Error

module DecimalNumber =
    type DecimalNumber(coreNumber: string) =
        let coreNumber = FormatDecimal coreNumber

        do
            if not (IsDecimal coreNumber) then
                raise (NotANumber("Not A Number"))

        override this.ToString() = coreNumber

        interface IComparable<DecimalNumber> with
            member this.CompareTo obj =
                let num1 = this.ToString()
                let num2 = obj.ToString()
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

        member this.IsLessThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.ToString())
            if result = -1 then true else false

        static member op_LessThan(number1: DecimalNumber, number2: DecimalNumber) = number1.IsLessThan(number2)

        member this.IsEqual(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.ToString())
            if result = 0 then true else false

        static member op_Equality(number1: DecimalNumber, number2: DecimalNumber) = number1.IsEqual(number2)

        member this.IsGresterThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.ToString())
            if result = 1 then true else false

        static member op_GreaterThan(number1: DecimalNumber, number2: DecimalNumber) = number1.IsGresterThan(number2)

        static member IsDecimal(number: string) = IsDecimal number

        static member Parst(coreNumber: string) = DecimalNumber(coreNumber)

        static member Compare (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            DecimalCompare sNumber1 sNumber2

        static member (~-)(number: DecimalNumber) =
            let sNumber = number.ToString()

            match sNumber[0] with
            | '-' -> sNumber[1..] |> DecimalNumber
            | _ -> ("-" + sNumber) |> DecimalNumber

        static member Add (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (AddDecimal sNumber1 sNumber2) |> DecimalNumber

        static member (+)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Add number1 number2

        static member Subtract (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (SubtractDecimal sNumber1 sNumber2)
            |> DecimalNumber

        static member (-)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Subtract number1 number2

        static member Multiply10 (number1: DecimalNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (MultiplyDecimal10 sNumber1 sNumber2)
            |> DecimalNumber

        static member (.*)(number1: DecimalNumber, number2: UIntNumber) =
            DecimalNumber.Multiply10 number1 number2

        static member Multiply (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (MultiplyDecimal sNumber1 sNumber2)
            |> DecimalNumber

        static member (*)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Multiply number1 number2

        static member Divide10 (number1: DecimalNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (MultiplyDecimal10 sNumber1 "-" + sNumber2)
            |> DecimalNumber

        static member (./)(number1: DecimalNumber, number2: UIntNumber) = DecimalNumber.Divide10 number1 number2

        static member Divide (number1: DecimalNumber) (number2: DecimalNumber) (accuracy: int) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (DivideDecimal sNumber1 sNumber2 accuracy)
            |> DecimalNumber

        static member (/)(number1: DecimalNumber, number2: DecimalNumber) = DecimalNumber.Divide number1 number2 10
