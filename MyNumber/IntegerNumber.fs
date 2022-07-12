namespace MyNumber

open MyNumber.Error
open MyNumber.Service.Integer

module Integer =
    type BaseIntegerNumber(coreNumber: string) =
        let coreNumber = coreNumber
        override this.ToString() = coreNumber

    type UIntNumber(coreNumber: string) =
        inherit BaseIntegerNumber(coreNumber)

        do
            if IsUInt coreNumber then
                raise (NotANumber("Not A Number"))

        static member IsNumber(number: string) = IsUInt number

        static member Parse(coreNumber: string) = UIntNumber(coreNumber)

        static member Add (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            let result = AddIntegerNumber sNumber1 sNumber2
            UIntNumber(result)


        static member Subtract (number1: UIntNumber) (number2: UIntNumber) =
            let sNumber1 = number1.ToString()
            let sNumber2 = number2.ToString()
            let len1 = sNumber1.Length
            let len2 = sNumber2.Length
            let mutable result = ""
            let mutable count = 1
            let mutable remain = 0

            while (count <= len1) && (count <= len2) do
                let n1 = int sNumber1[len1 - count] - 48
                let n2 = int sNumber2[len2 - count] - 48

                if n1 >= n2 + remain then
                    result <- (string (n1 - n2 - remain)) + result
                    remain <- 0
                else
                    result <- (string (n1 + 10 - n2 - remain)) + result
                    remain <- 1

                count <- count + 1

            while count <= len1 do
                let n1 = int sNumber1[len1 - count] - 48

                if n1 >= remain then
                    result <- (string (n1 - remain)) + result
                    remain <- 0
                else
                    result <- (string (10 + n1 - remain)) + result
                    remain <- 1

                count <- count + 1

            UIntNumber(result)


        static member Multiply (number1: UIntNumber) (number2: UIntNumber) = ()

        static member Divide (number1: UIntNumber) (number2: UIntNumber) = ()

    type IntNumber(coreNumber: string) =
        inherit BaseIntegerNumber(coreNumber)

        do
            if IsInt coreNumber then
                raise (NotANumber("Not A Number"))

        static member IsNumber(number: string) = IsInt number

        static member Parse(coreNumber: string) = IntNumber(coreNumber)

        static member Add (number1: IntNumber) (number2: IntNumber) = ()

        static member Subtract (number1: IntNumber) (number2: IntNumber) = ()

        static member Multiply (number1: IntNumber) (number2: IntNumber) = ()

        static member Divide (number1: IntNumber) (number2: IntNumber) = ()
