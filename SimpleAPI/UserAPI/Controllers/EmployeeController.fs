namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Services
open UserAPI.Connector

open UserAPI.Models.SqlModel

[<ApiController>]
type EmployeeController =
    inherit ControllerBase

    val logger: ILogger<EmployeeController>
    val employeeService: SqlService<Employee>

    new(logger: ILogger<EmployeeController>) =
        { inherit ControllerBase()
          logger = logger
          employeeService = new SqlService<Employee>(APIConnection.Connection.SQL.SqlData.Employees) }

    [<HttpPost("/employee/login")>]
    member this.Login([<FromBody>] info: LoginEmployeeInfo) =
        this.employeeService.SelectWithFilter($"WHERE Username=%s{info.Username} AND Password=%s{info.Password}")


    [<HttpGet("/employee/{employeeId}")>]
    member this.GetEmployeeById (emplyeeId: string) ([<FromQuery>] fileds: string) =
        this.employeeService.SelectWithFilter $"WHERE Id=%s{emplyeeId}" fileds
