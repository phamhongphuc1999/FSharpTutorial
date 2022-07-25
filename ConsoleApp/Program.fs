open MyNumber.Number.DecimalNumber

let num1 = DecimalNumber "12.34"
let num2 = DecimalNumber "12.1"

printfn "%s" (num1 + num2).CoreNumber
printfn "%s" (num1 - num2).CoreNumber
printfn "%s" (num2 - num1).CoreNumber
printfn "%s" (num1 * num2).CoreNumber
printfn "%s" (num1 / num2).CoreNumber
