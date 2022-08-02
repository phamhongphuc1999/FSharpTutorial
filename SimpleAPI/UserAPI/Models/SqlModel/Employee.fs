namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Employees")>]
type Employee() =
    [<TableRow("id")>]
    member val Id: int32 = 0 with get, set

    [<TableRow("username")>]
    member val Username: string = "" with get, set

    [<TableRow("password")>]
    member val Password: string = "" with get, set

    [<TableRow("email")>]
    member val Email: string = "" with get, set
