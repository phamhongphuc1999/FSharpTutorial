namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector

[<ApiController>]
[<Route("/employee")>]
type EmployeeController(logger: ILogger<EmployeeController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetListEmployees() =
        let employees = APIConnection.Connection.SQL.SqlData.Employees
        employees.SelectAll()
