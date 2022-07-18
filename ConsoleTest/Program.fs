open MyNumber.DecimalNumber

let num1 = DecimalNumber("123456789")
let num2 = DecimalNumber("55")

printfn "%s" ((num1 / num2).ToString())
