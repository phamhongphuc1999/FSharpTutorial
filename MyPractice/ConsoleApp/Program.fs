open MyNumber.Number.UIntNumber
open MyNumber.Number.IntNumber
open MyNumber.Number.DecimalNumber
open MyNumber.Number.ComplexNumber

let num2 = ("1", "2") |> ComplexNumber

printfn "%s" (num2 * num2 * num2).CoreNumber
printfn "%s" (num2 .^ ("10" |> IntNumber)).CoreNumber
