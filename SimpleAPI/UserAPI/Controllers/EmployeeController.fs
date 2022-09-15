namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector

open UserAPI.Models.SqlModel

[<ApiController>]
type EmployeeController =
    inherit ControllerBase

    val logger: ILogger<EmployeeController>
    val employeeModel: DataSet.SqlDataSet<Employee>

    new(logger: ILogger<EmployeeController>) =
        { inherit ControllerBase()
          logger = logger
          employeeModel = APIConnection.Connection.SQL.SqlData.Employees }

    [<HttpPost("/employee/login")>]
    member this.Login([<FromBody>] info: LoginEmployeeInfo) =
        this.employeeModel.SelectWithFilter($"WHERE Username=%s{info.Username} AND Password=%s{info.Password}")


    [<HttpGet("/employee-list")>]
    member this.GetListEmployees() = this.employeeModel.SelectAll()
