namespace MyNumber

open MyNumber.Error
open MyNumber.Service.UInt
open MyNumber.Service.Int

type BaseIntegerNumber(coreNumber: string) =
    let coreNumber = FormatUInt coreNumber
    override this.ToString() = coreNumber

module UIntNumber =
    type UIntNumber(coreNumber: string) =

        inherit BaseIntegerNumber(coreNumber)

        do
            if not (IsUInt coreNumber) then
                raise (NotANumber("Not A Number"))

        member this.IsLessThan(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result = -1 then true else false

        member this.IsEqual(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result = 0 then true else false

        member this.IsGresterThan(number: UIntNumber) =
            let result = UIntCompare coreNumber (number.ToString())
            if result = 1 then true else false

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

        static member Subtract (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (SubtractUInt sNumber1 sNumber2) |> UIntNumber

        static member Multiply (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyUInt sNumber1 sNumber2) |> UIntNumber

        static member Divide (dividend: UIntNumber) (divisor: UIntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            (DivideUInt sDividend sDivisor) |> UIntNumber

        static member DivideMod (dividend: UIntNumber) (divisor: UIntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            (DivideModUInt sDividend sDivisor) |> UIntNumber

        static member RealDivide (dividend: UIntNumber) (divisor: UIntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            let (integerPart, decimalPart) = RealDivideUInt sDividend sDivisor
            (integerPart |> UIntNumber, decimalPart |> UIntNumber)

        static member Multiply10 (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyUInt10 sNumber1 sNumber2) |> UIntNumber

        static member Pow (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (PowUInt sNumber1 sNumber2) |> UIntNumber

module IntNumber =
    type IntNumber(coreNumber: string) =
        inherit BaseIntegerNumber(coreNumber)

        do
            if not (IsInt coreNumber) then
                raise (NotANumber("Not A Number"))

        member this.IsLessThan(number: IntNumber) =
            let result = IntCompare coreNumber (number.ToString())
            if result = -1 then true else false

        member this.IsEqual(number: IntNumber) =
            let result = IntCompare coreNumber (number.ToString())
            if result = 0 then true else false

        member this.IsGresterThan(number: IntNumber) =
            let result = IntCompare coreNumber (number.ToString())
            if result = 1 then true else false

        member this.GetUInt() =
            let (sign, uintNumber) = GetUIntNumber coreNumber
            (sign, UIntNumber.UIntNumber(uintNumber))

        static member IsNumber(number: string) = IsInt number

        static member Parse(coreNumber: string) = IntNumber(coreNumber)

        static member Compare (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            IntCompare sNumber1 sNumber2

        static member Add (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (AddInt sNumber1 sNumber2) |> IntNumber

        static member Subtract (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (SubtractInt sNumber1 sNumber2) |> IntNumber

        static member Multiply (number1: IntNumber) (number2: IntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyInt sNumber1 sNumber2) |> IntNumber

        static member Divide (dividend: IntNumber) (divisor: IntNumber) =
            let sDividend = dividend.ToString()
            let sDivisor = divisor.ToString()
            (DivideInt sDividend sDivisor) |> IntNumber

        static member Multiply10 (number1: IntNumber) (number2: UIntNumber.UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (MultiplyInt10 sNumber1 sNumber2) |> IntNumber

        static member Pow (number1: IntNumber) (number2: UIntNumber.UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (PowInt sNumber1 sNumber2) |> IntNumber
