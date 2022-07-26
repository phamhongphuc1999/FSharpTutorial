open MyNumber.Number.UIntNumber
open MyNumber.Number.IntNumber
open MyNumber.Number.DecimalNumber
open MyNumber.Number.ComplexNumber
open MyNumber.Number.FractionNumber

let num1 = FractionNumber("-3", "9")
let num2 = FractionNumber("1", "2")
let num3 = DecimalNumber "-123.16151718"

printfn "%s" (num1.ToString())
printfn "add: %s" ((num1 + num2).ToString())
printfn "subtract: %s" ((num1 - num2).ToString())
printfn "multiple: %s" ((num1 * num2).ToString())
printfn "divide: %s" ((num1 / num2).ToString())
printfn "%s" (num3.Ceiling 3).CoreNumber
