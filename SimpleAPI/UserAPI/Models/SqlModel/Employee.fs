namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Employees")>]
type Employee() =
    member val id: int32 = 0 with get, set
    member val username: string = "" with get, set
    member val password: string = "" with get, set
    member val email: string = "" with get, set

type LoginEmployeeInfo() =
    member val username: string = "" with get, set
    member val password: string = "" with get, set

type RegisterEmployeeInfo() =
    member val username: string = "" with get, set
    member val password: string = "" with get, set
    member val email: string = "" with get, set
