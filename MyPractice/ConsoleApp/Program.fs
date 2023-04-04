open MyNumber.Service.Decimal

[<EntryPoint>]
let main args =
    let (sign: int, integer: string, decimal: string) = DeepGetIntegerAndDecimal "0.12"
    printfn "%i" sign
    printfn "%s" integer
    printfn "%s" decimal
    0
