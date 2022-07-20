open MyNumber.Number.DecimalNumber

let num1 = DecimalNumber("123456789")
let num2 = DecimalNumber("5")

printfn "divide: %s" ((num1 / num2).ToString())
