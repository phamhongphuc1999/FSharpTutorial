namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Employees")>]
type Employee() =
    member val Id: int32 = 0 with get, set
    member val Username: string = "" with get, set
    member val Password: string = "" with get, set
    member val Email: string = "" with get, set
