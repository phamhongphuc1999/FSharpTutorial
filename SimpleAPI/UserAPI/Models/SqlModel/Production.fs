namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Productions")>]
type Production() =
    member val id: int32 = 0 with get, set
    member val name: string = "" with get, set
    member val amount: int32 = 0 with get, set
    member val employeeId: int32 = 0 with get, set
