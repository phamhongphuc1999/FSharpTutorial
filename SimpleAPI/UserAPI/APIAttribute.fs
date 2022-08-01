namespace UserAPI

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
