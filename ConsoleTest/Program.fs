open MyNumber.Integer

let number1 = UIntNumber("321")
let number2 = UIntNumber.Parse("123")

printfn "%s" ((UIntNumber.Subtract number1 number2).ToString())
