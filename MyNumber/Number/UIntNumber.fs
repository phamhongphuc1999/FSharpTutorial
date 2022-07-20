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
            and set value = coreNumber <- value

        interface IComparable<UIntNumber> with
            member this.CompareTo obj =
                let num1 = this.ToString()
                let num2 = obj.ToString()
                UIntCompare num1 num2

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
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            UIntCompare sNumber1 sNumber2

        static member Add (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (AddUInt sNumber1 sNumber2) |> UIntNumber

        static member (+)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.Add number1 number2

        static member Subtract (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (SubtractUInt sNumber1 sNumber2) |> UIntNumber

        static member (-)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.Subtract number1 number2

        static member Multiply (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyUInt sNumber1 sNumber2) |> UIntNumber

        static member (*)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.Multiply number1 number2

        static member Divide (dividend: UIntNumber) (divisor: UIntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            (DivideUInt sDividend sDivisor) |> UIntNumber

        static member (/)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.Divide number1 number2

        static member DivideMod (dividend: UIntNumber) (divisor: UIntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            (DivideModUInt sDividend sDivisor) |> UIntNumber

        static member (%)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.DivideMod number1 number2

        static member RealDivide (dividend: UIntNumber) (divisor: UIntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            let (integerPart, decimalPart) = RealDivideUInt sDividend sDivisor
            (integerPart |> UIntNumber, decimalPart |> UIntNumber)

        static member (/%)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.RealDivide number1 number2

        static member Multiply10 (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyUInt10 sNumber1 sNumber2) |> UIntNumber

        static member (.*)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.Multiply10 number1 number2

        static member Pow (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (PowUInt sNumber1 sNumber2) |> UIntNumber

        static member (.^)(number1: UIntNumber, number2: UIntNumber) = UIntNumber.Pow number1 number2
