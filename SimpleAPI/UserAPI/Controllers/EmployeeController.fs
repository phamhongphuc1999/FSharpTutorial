namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Services
open UserAPI.Models.SqlModel

[<Produces("application/json")>]
[<Consumes("application/json")>]
[<ApiController>]
type EmployeeController =
    inherit ControllerBase

    val logger: ILogger<EmployeeController>
    val employeeService: EmployeeService

    new(logger: ILogger<EmployeeController>) =
        { inherit ControllerBase()
          logger = logger
          employeeService = new EmployeeService() }

    [<HttpPost("/employee/login")>]
    member this.Login([<FromBody>] info: LoginEmployeeInfo) =
        this.employeeService.Login info.username info.password


    [<HttpGet("/employee")>]
    member this.GetEmployeeById ([<FromQuery>] employeeId: string) ([<FromQuery>] fileds: string) =
        this.employeeService.SelectEmployeeById employeeId fileds
