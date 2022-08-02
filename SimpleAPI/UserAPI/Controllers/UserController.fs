namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector
open UserAPI.Models.SqlModel

[<ApiController>]
[<Route("/user")>]
type UserController(logger: ILogger<UserController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetUser() =
        let sqlConnection = APIConnection.Connection.SQL.Connection
        //let dataSet = new SqlDataSet<Employee>(sqlConnection)
        //let re = dataSet.GetReflection()
        //printfn "%A" re
        //let re1 = dataSet.GetTableSchema("Employees")
        //printfn "%A" re1
        [ 1; 2; 3; 4; 5; 6 ]
