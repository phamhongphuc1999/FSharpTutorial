open MyNumber.Number.DecimalNumber
open MyNumber.Number.IntNumber
open MyNumber.Number.UIntNumber

let num1 = DecimalNumber "2.2"
let num2 = IntNumber "-3"
let num3 = UIntNumber "100"

printfn "%s" (num1 .^ num2).CoreNumber
printfn "%s" (num3.Factorial().CoreNumber)
