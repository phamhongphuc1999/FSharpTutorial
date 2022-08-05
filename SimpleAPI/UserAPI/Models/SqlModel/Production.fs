namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Productions")>]
type Production() =
    [<TableRow("name")>]
    member val Name: string = "" with get, set

    [<TableRow("amount")>]
    member val Amount: int32 = 0 with get, set
