namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Services
open UserAPI.Models.SqlModel
open UserAPI.Models

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

    /// <summary>login</summary>
    /// <remarks>login</remarks>
    /// <returns>The token and basic information of user</returns>
    /// <response code="200">return the new access token or announce already login</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">username or password is wrong</response>
    /// <response code="403">This account is enable to login</response>
    [<HttpPost("/login")>]
    [<ProducesResponseType(201, Type = typeof<ResponseSuccessType>)>]
    [<ProducesResponseType(400, Type = typeof<ResponseFailType>)>]
    member this.Login([<FromBody>] info: LoginEmployeeInfo) =
        this.employeeService.Login info.username info.password

    /// <summary>create new user</summary>
    /// <remarks>create new user</remarks>
    /// <param name="info">the information of new user you want to add in your database</param>
    /// <returns></returns>
    /// <response code="200">return information of new user</response>
    /// <response code="400">if get mistake</response>
    [<HttpPost("/register")>]
    [<ProducesResponseType(200, Type = typeof<ResponseSuccessType>)>]
    [<ProducesResponseType(400, Type = typeof<ResponseFailType>)>]
    member this.Register([<FromBody>] info: RegisterEmployeeInfo) =
        this.employeeService.Register info.username info.password info.email

    /// <summary>get user by id</summary>
    /// <remarks>get user by id</remarks>
    /// <param name="employeeId">the id of employee you want to get</param>
    /// <param name="fields">the specified fields you want to get</param>
    /// <returns></returns>
    /// <response code="200">return information of user with specified fields</response>
    /// <response code="400">if get mistake</response>
    [<HttpGet("/employee/{employeeId}")>]
    [<ProducesResponseType(200, Type = typeof<ResponseSuccessType>)>]
    [<ProducesResponseType(400, Type = typeof<ResponseFailType>)>]
    member this.GetEmployeeById (employeeId: string) ([<FromQuery>] fileds: string) =
        this.employeeService.GetEmployeeById employeeId fileds

    [<HttpGet("/employee-list")>]
    member this.GetAllEmployees([<FromQuery>] fileds: string) =
        this.employeeService.SelectAll fileds
