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

        new(sign: int, number: UIntNumber) =
            if sign < 0 then
                IntNumber("-" + number.CoreNumber)
            else
                IntNumber number.CoreNumber

        member this.CoreNumber
            with get () = coreNumber
            and set value = coreNumber <- FormatInt value

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

        member this.IsNotEqual(number: IntNumber) =
            let result = IntCompare this.CoreNumber number.CoreNumber
            if result <> 0 then true else false

        static member op_Inequality(number1: IntNumber, number2: IntNumber) = number1.IsNotEqual(number2)

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
            let sNumber1 = number1.CoreNumber
            let sNumber2 = number2.CoreNumber
            IntCompare sNumber1 sNumber2

        static member (~+)(number: IntNumber) = number

        static member (~-)(number: IntNumber) =
            let sNumber = number.ToString()

            match sNumber[0] with
            | '-' -> sNumber[1..] |> IntNumber
            | _ -> ("-" + sNumber) |> IntNumber

        member this.Abs() =
            let sNum = this.CoreNumber

            match sNum[0] with
            | '-' -> sNum[1..] |> UIntNumber
            | _ -> sNum |> UIntNumber

        member this.Add(number: IntNumber) =
            (AddInt this.CoreNumber number.CoreNumber)
            |> IntNumber

        static member (+)(number1: IntNumber, number2: IntNumber) = number1.Add number2

        static member (+=)(number1: IntNumber, number2: IntNumber) =
            number1.CoreNumber <- AddInt number1.CoreNumber number2.CoreNumber

        static member (+)(number1: UIntNumber, number2: IntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> AddInt
            |> IntNumber

        static member (+)(number1: IntNumber, number2: UIntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> AddInt
            |> IntNumber

        member this.Subtract(number: IntNumber) =
            (SubtractInt this.CoreNumber number.CoreNumber)
            |> IntNumber

        static member (-)(number1: IntNumber, number2: IntNumber) = number1.Subtract number2

        static member (-=)(number1: IntNumber, number2: IntNumber) =
            number1.CoreNumber <- SubtractInt number1.CoreNumber number2.CoreNumber

        static member (-)(number1: IntNumber, number2: UIntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> SubtractInt
            |> IntNumber

        static member (-)(number1: UIntNumber, number2: IntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> SubtractInt
            |> IntNumber

        member this.Multiply(number: IntNumber) =
            (MultiplyInt this.CoreNumber number.CoreNumber)
            |> IntNumber

        static member (*)(number1: IntNumber, number2: IntNumber) = number1.Multiply number2

        static member op_MultiplyAssignment(number1: IntNumber, number2: IntNumber) =
            number1.CoreNumber <- (MultiplyUInt number1.CoreNumber number2.CoreNumber)

        static member (*)(number1: IntNumber, number2: UIntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> MultiplyInt
            |> IntNumber

        static member (*)(number1: UIntNumber, number2: IntNumber) =
            (number1.CoreNumber, number2.CoreNumber)
            ||> MultiplyInt
            |> IntNumber

        member this.Divide(divisor: IntNumber) =
            (DivideInt this.CoreNumber divisor.CoreNumber)
            |> IntNumber

        static member (/)(dividend: IntNumber, divisor: IntNumber) = dividend.Divide divisor

        static member (/=)(dividend: IntNumber, divisor: IntNumber) =
            dividend.CoreNumber <- (DivideUInt dividend.CoreNumber divisor.CoreNumber)

        static member (/)(dividend: UIntNumber, divisor: IntNumber) =
            (DivideInt dividend.CoreNumber divisor.CoreNumber)
            |> IntNumber

        static member (/)(dividend: IntNumber, divisor: UIntNumber) =
            (DivideInt dividend.CoreNumber divisor.CoreNumber)
            |> IntNumber

        member this.Multiply10(number: UIntNumber) =
            (MultiplyInt10 this.CoreNumber number.CoreNumber)
            |> IntNumber

        static member (.*)(number1: IntNumber, number2: UIntNumber) = number1.Multiply10 number2

        member this.Pow(number: UIntNumber) =
            (PowInt this.CoreNumber number.CoreNumber)
            |> IntNumber

        static member (.^)(number1: IntNumber, number2: UIntNumber) = number1.Pow number2
