namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector

[<ApiController>]
[<Route("/user")>]
type UserController(logger: ILogger<UserController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetListEmployees() =
        let employees = APIConnection.Connection.SQL.SqlData.Employees
        employees.SelectAll()
