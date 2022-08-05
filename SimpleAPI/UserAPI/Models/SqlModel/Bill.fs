namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Bills")>]
type Bill() =
    [<TableRow("id")>]
    member val Id: int32 = 0 with get, set

    [<TableRow("employeeId")>]
    member val EmployeeId: int32 = 0 with get, set

    [<TableRow("productionId")>]
    member val ProductionId: string = "" with get, set
