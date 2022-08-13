namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Bills")>]
type Bill() =
    member val Id: int32 = 0 with get, set
    member val EmployeeId: int32 = 0 with get, set
    member val ProductionId: string = "" with get, set
    member val DaySell: string = "" with get, set
    member val Status: int32 = 0 with get, set
