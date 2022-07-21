open MyNumber.Number.DecimalNumber

let num1 = DecimalNumber("123456789")
let num2 = DecimalNumber("5.512345")

printfn "%s" num2.CoreNumber

num2.Ceiling 1

printfn "%s" num2.CoreNumber
