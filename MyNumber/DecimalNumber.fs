namespace MyNumber

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

        member this.IsLessThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.ToString())
            if result = -1 then true else false

        member this.IsEqual(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.ToString())
            if result = 0 then true else false

        member this.GrestThan(number: DecimalNumber) =
            let result = DecimalCompare coreNumber (number.ToString())
            if result = 1 then true else false

        static member IsDecimal(number: string) = IsDecimal number

        static member Parst(coreNumber: string) = DecimalNumber(coreNumber)

        static member Compare (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            DecimalCompare sNumber1 sNumber2

        static member Add (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            (AddDecimal sNumber1 sNumber2) |> DecimalNumber

        static member Subtract (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (SubtractDecimal sNumber1 sNumber2)
            |> DecimalNumber

        static member Multiply10 (number1: DecimalNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (MultiplyDecimal10 sNumber1 sNumber2)
            |> DecimalNumber

        static member Multiply (number1: DecimalNumber) (number2: DecimalNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (MultiplyDecimal sNumber1 sNumber2)
            |> DecimalNumber

        static member Divide10 (number1: DecimalNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()

            (MultiplyDecimal10 sNumber1 "-" + sNumber2)
            |> DecimalNumber
