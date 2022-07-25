open MyNumber.Number.ComplexNumber

let num1 = ComplexNumber("123", "123")
let num2 = ComplexNumber("1", "1")

printfn "%s" (num1 + num2).CoreNumber
printfn "%s" (num1 - num2).CoreNumber
printfn "%s" (num1 / num2).CoreNumber
