namespace MyNumber.Number

open System
open MyNumber.Error
open MyNumber.Service.UInt
open MyNumber.Service.Int
open MyNumber.Number.UIntNumber

module IntNumber =
    type IntNumber(coreNumber: string) =
        let mutable coreNumber = FormatInt coreNumber

        do
            if not (IsInt coreNumber) then
                raise (NotANumber("Not A Number"))

        member this.CoreNumber
            with get () = coreNumber
            and set value = coreNumber <- value

        interface IComparable<IntNumber> with
            member this.CompareTo obj =
                let num1 = this.ToString()
                let num2 = obj.ToString()
                UIntCompare num1 num2

        interface IComparable with
            member this.CompareTo obj =
                match obj with
                | null -> 1
                | :? IntNumber as other -> (this :> IComparable<_>).CompareTo other
                | _ -> invalidArg "obj" "not a Category"

        interface IEquatable<IntNumber> with
            member this.Equals obj = this.IsEqual(obj)

        override this.ToString() = coreNumber

        override this.Equals obj =
            match obj with
            | :? IntNumber as other -> (this :> IEquatable<_>).Equals other
            | _ -> false

        override this.GetHashCode() = this.GetHashCode()

        member this.IsLessThan(number: IntNumber) =
            let result = IntCompare coreNumber (number.ToString())
            if result = -1 then true else false

        static member op_LessThan(number1: IntNumber, number2: IntNumber) = number1.IsLessThan(number2)

        member this.IsEqual(number: IntNumber) =
            let result = IntCompare coreNumber (number.ToString())
            if result = 0 then true else false

        static member op_Equality(number1: IntNumber, number2: IntNumber) = number1.IsEqual(number2)

        member this.IsGresterThan(number: IntNumber) =
            let result = IntCompare coreNumber (number.ToString())
            if result = 1 then true else false

        static member op_GreaterThan(number1: IntNumber, number2: IntNumber) = number1.IsGresterThan(number2)

        member this.GetUInt() =
            let (sign, uintNumber) = GetUIntNumber coreNumber
            (sign, uintNumber |> UIntNumber.UIntNumber)

        static member IsNumber(number: string) = IsInt number

        static member Parse(coreNumber: string) = IntNumber(coreNumber)

        static member Compare (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            IntCompare sNumber1 sNumber2

        static member (~-)(number: IntNumber) =
            let sNumber = number.ToString()

            match sNumber[0] with
            | '-' -> sNumber[1..] |> IntNumber
            | _ -> ("-" + sNumber) |> IntNumber

        static member Add (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (AddInt sNumber1 sNumber2) |> IntNumber

        static member (+)(number1: IntNumber, number2: IntNumber) = IntNumber.Add number1 number2

        static member Subtract (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (SubtractInt sNumber1 sNumber2) |> IntNumber

        static member (-)(number1: IntNumber, number2: IntNumber) = IntNumber.Subtract number1 number2

        static member Multiply (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyInt sNumber1 sNumber2) |> IntNumber

        static member (*)(number1: IntNumber, number2: IntNumber) = IntNumber.Multiply number1 number2

        static member Divide (dividend: IntNumber) (divisor: IntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            (DivideInt sDividend sDivisor) |> IntNumber

        static member (/)(number1: IntNumber, number2: IntNumber) = IntNumber.Divide number1 number2

        static member Multiply10 (number1: IntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyInt10 sNumber1 sNumber2) |> IntNumber

        static member (.*)(number1: IntNumber, number2: UIntNumber) = IntNumber.Multiply10 number1 number2

        static member Pow (number1: IntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (PowInt sNumber1 sNumber2) |> IntNumber

        static member (.^)(number1: IntNumber, number2: UIntNumber) = IntNumber.Pow number1 number2
