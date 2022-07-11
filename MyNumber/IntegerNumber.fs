module MyNumber.Integer

type UIntNumber(coreNumber: string) =
    let coreNumber = coreNumber

    member this.CoreNumber = coreNumber

    static member IsNumber(number: string) =
        let mutable check = true
        let len = number.Length
        let mutable count = 0

        while (count < len) && check do
            let unitC = int number[count]

            if 48 >= unitC || 57 <= unitC then
                check <- false
            else
                count <- count + 1

        check


    static member Parse(coreNumber: string) = UIntNumber(coreNumber)

    static member Add (number1: string) (number2: string) =
        let check =
            (UIntNumber.IsNumber number1)
            && (UIntNumber.IsNumber number2)

        if not check then
            raise (System.ArgumentException("Divisor cannot be zero!"))

        let len1 = number1.Length
        let len2 = number2.Length
        let mutable result = ""
        let mutable count = 1
        let mutable remain = 0

        while (count <= len1) && (count <= len2) do
            let n1 = int number1[len1 - count]
            let n2 = int number2[len2 - count]
            let temp = n1 + n2 + remain
            result <- (string (temp / 10)) + result
            remain <- temp % 10
            count <- count + 1

        while count <= len1 do
            let n1 = int number1[len1 - count]
            let temp = n1 + remain
            result <- string (temp / 10) + result
            remain <- temp % 10
            count <- count + 1

        while count <= len2 do
            let n2 = int number2[len2 - count]
            let temp = n2 + remain
            result <- string (temp / 10) + result
            remain <- temp % 10
            count <- count + 1

        if remain > 0 then
            result <- (string remain) + result

        result


    static member Subtract (number1: string) (number2: string) = ()

    static member Multiply (number1: string) (number2: string) = ()

    static member Divide (number1: string) (number2: string) = ()
