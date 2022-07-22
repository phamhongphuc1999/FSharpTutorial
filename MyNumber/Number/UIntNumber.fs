namespace MyNumber.Number

open System
open MyNumber.Error
open MyNumber.Service.UInt

module UIntNumber =
    type UIntNumber(coreNumber: string) =
        let mutable coreNumber = FormatUInt coreNumber

        do
            if not (IsUInt coreNumber) then
                raise (NotANumber("Not A Number"))

        member this.CoreNumber
            with get () = coreNumber
            and set value = coreNumber <- FormatUInt value

        interface IComparable<UIntNumber> with
            member this.CompareTo obj =
                (this.CoreNumber, obj.CoreNumber) ||> UIntCompare

        interface IComparable with
            member this.CompareTo obj =
                match obj with
                | null -> 1
                | :? UIntNumber as other -> (this :> IComparable<_>).CompareTo other
                | _ -> invalidArg "obj" "not a Category"

        interface IEquatable<UIntNumber> with
            member this.Equals obj = this.IsEqual(obj)

        override this.Equals obj =
            match obj with
            | :? UIntNumber as other -> (this :> IEquatable<_>).Equals other
            | _ -> false

        override this.GetHashCode() = this.GetHashCode()

        override this.ToString() = coreNumber

        member this.IsLessThan(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result = -1 then true else false

        static member op_LessThan(number1: UIntNumber, number2: UIntNumber) = number1.IsLessThan(number2)

        member this.IsLessThanOrEqual(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result <= 0 then true else false

        static member op_LessThanOrEqual(number1: UIntNumber, number2: UIntNumber) = number1.IsLessThanOrEqual(number2)

        member this.IsEqual(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result = 0 then true else false

        static member op_Equality(number1: UIntNumber, number2: UIntNumber) = number1.IsEqual(number2)

        member this.IsGresterThan(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result = 1 then true else false

        static member op_GreaterThan(number1: UIntNumber, number2: UIntNumber) = number1.IsGresterThan(number2)

        member this.IsGreaterThanOrEqual(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result >= 0 then true else false

        static member op_GreaterThanOrEqual(number1: UIntNumber, number2: UIntNumber) =
            number1.IsGreaterThanOrEqual(number2)

        static member IsNumber(number: string) = IsUInt number

        static member Parse(coreNumber: string) = UIntNumber(coreNumber)

        static member Compare (number1: UIntNumber) (number2: UIntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> UIntCompare

        member this.Add(number: UIntNumber) =
            (this.CoreNumber, number.CoreNumber)
            ||> AddUInt
            |> UIntNumber

        static member (+)(number1: UIntNumber, number2: UIntNumber) = number1.Add number2

        member this.Subtract(number: UIntNumber) =
            (this.CoreNumber, number.CoreNumber)
            ||> SubtractUInt
            |> UIntNumber

        static member (-)(number1: UIntNumber, number2: UIntNumber) = number1.Subtract number2

        member this.Multiply(number: UIntNumber) =
            (this.CoreNumber, number.CoreNumber)
            ||> MultiplyUInt
            |> UIntNumber

        static member (*)(number1: UIntNumber, number2: UIntNumber) = number1.Multiply number2

        member this.Divide(divisor: UIntNumber) =
            (DivideUInt this.CoreNumber divisor.CoreNumber)
            |> UIntNumber

        static member (/)(dividend: UIntNumber, divisor: UIntNumber) = dividend.Divide divisor

        member this.DivideMod(divisor: UIntNumber) =
            (DivideModUInt this.CoreNumber divisor.CoreNumber)
            |> UIntNumber

        static member (%)(dividend: UIntNumber, divisor: UIntNumber) = dividend.DivideMod divisor

        member this.RealDivide(divisor: UIntNumber) =
            let (integerPart, decimalPart) = RealDivideUInt this.CoreNumber divisor.CoreNumber
            (integerPart |> UIntNumber, decimalPart |> UIntNumber)

        static member (/%)(dividend: UIntNumber, divisor: UIntNumber) = dividend.RealDivide divisor

        member this.Multiply10(number: UIntNumber) =
            (MultiplyUInt10 this.CoreNumber number.CoreNumber)
            |> UIntNumber

        static member (.*)(number1: UIntNumber, number2: UIntNumber) = number1.Multiply10 number2

        member this.Pow(number: UIntNumber) =
            (PowUInt this.CoreNumber number.CoreNumber)
            |> UIntNumber

        static member (.^)(number1: UIntNumber, number2: UIntNumber) = number1.Pow number2
