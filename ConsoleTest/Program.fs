open MyNumber.Number.IntNumber
open MyNumber.Number.UIntNumber

let num1 = UIntNumber "123"
let num2 = IntNumber "-23"

printfn "%s" (num1 + num2).CoreNumber
printfn "%s" (num2 + num1).CoreNumber
