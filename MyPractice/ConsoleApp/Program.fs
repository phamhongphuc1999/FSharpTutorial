open System.ComponentModel
open System

type Table private () =
    inherit Attribute()
    member val Name: string = "" with get, set

    new(name: string) as this =
        Table()
        then this.Name <- name

type TableRow private () =
    inherit Attribute()
    member val Name: string = "" with get, set

    new(name: string) as this =
        TableRow()
        then this.Name <- name

[<Table("Employees")>]
type Employee() =
    member val Id: int32 = 0 with get, set

    [<TableRow("PHUC")>]
    member val Username: string = "" with get, set

    member val Password: string = "" with get, set
    member val Email: string = "" with get, set

[<EntryPoint>]
let Main args =
    let moduleInfo = typeof<Employee>
    printfn "%s" moduleInfo.Name

    let properties = moduleInfo.GetProperties()

    for _pro in properties do
        printfn "Property: %s, Type: %s" _pro.Name _pro.PropertyType.Name
        let proAttr = Attribute.GetCustomAttributes(_pro)

        for _attr in proAttr do
            if (_attr :? TableRow) then
                let a = _attr :?> TableRow
                printfn "Table Row: %A" a.Name

    let methods = moduleInfo.GetMethods()

    for _met in methods do
        printfn "Method: %s" _met.Name

    let attrs = Attribute.GetCustomAttributes(moduleInfo)

    for _attr in attrs do
        if (_attr :? Table) then
            let a = _attr :?> Table
            printfn "Table:%A" a.Name

    // let em1 = new Employee()
    // printfn "%s" (em1.Username)
    // em1.GetType().GetProperty("Username").SetValue(em1, "123")
    // printfn "%s" (em1.Username)

    0
