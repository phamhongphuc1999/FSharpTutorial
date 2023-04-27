namespace UserAPI.Models.SqlModel

open UserAPI
open System.ComponentModel.DataAnnotations

[<Table("Employees")>]
type Employee() =
    member val id: int32 = 0 with get, set

    [<StringLength(30)>]
    member val username: string = "" with get, set

    [<StringLength(100)>]
    member val password: string = "" with get, set

    [<StringLength(50)>]
    member val email: string = "" with get, set

type LoginEmployeeInfo() =
    member val username: string = "" with get, set
    member val password: string = "" with get, set

type RegisterEmployeeInfo() =
    [<Required(ErrorMessage = "Username is required")>]
    [<StringLength(30)>]
    member val username: string = "" with get, set

    [<Required(ErrorMessage = "Password is required")>]
    [<RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Minimum eight characters, at least one letter and one number")>]
    [<StringLength(100)>]
    member val password: string = "" with get, set

    [<EmailAddress>]
    member val email: string = "" with get, set
