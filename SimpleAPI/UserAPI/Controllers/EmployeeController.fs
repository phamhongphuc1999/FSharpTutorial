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

    [<HttpPost("/login")>]
    member this.Login([<FromBody>] info: LoginEmployeeInfo) =
        this.employeeService.Login info.username info.password

    [<HttpPost("/register")>]
    member this.Register([<FromBody>] info: RegisterEmployeeInfo) = 
        this.employeeService.Register info.username info.password info.email

    [<HttpGet("/employee/{employeeId}")>]
    member this.GetEmployeeById (employeeId: string) ([<FromQuery>] fileds: string) =
        this.employeeService.SelectEmployeeById employeeId fileds

    [<HttpGet("/employee-list")>]
    member this.GetAllEmployees([<FromQuery>] fileds: string) = 
        this.employeeService.SelectAll fileds
