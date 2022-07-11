open MyNumber.Integer
open MyNumber.Error

let number1 = UIntNumber("123")
let number2 = UIntNumber.Parse("321")

printf "%s" (UIntNumber.Add "1234" "1234")
