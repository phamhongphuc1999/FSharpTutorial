open System

[<EntryPoint>]
let main args =
    Console.WriteLine("The Simple Calculator")
    let FUN = [ "add"; "sub"; "mul"; "div" ]
    let MODE = [ "uint"; "int"; "decimal"; "fraction"; "complex" ]
    let check: bool = true

    while check do
        let command = Console.ReadLine()
        let _command = command.Split([| ' ' |])

        if _command[0] <> "fp" then
            printfn "Command do not found"

        let _function = _command[1]
        let _mode = _command[2]

        if not (List.contains _function FUN) then
            printfn "function do not found"

        if not (List.contains _mode MODE) then
            printfn "mode do not found"

        printfn "%s" _command[3]

    0
