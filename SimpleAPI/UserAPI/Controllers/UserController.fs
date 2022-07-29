namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector
open MySqlConnector

[<ApiController>]
[<Route("/user")>]
type UserController(logger: ILogger<UserController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetUser() =
        printfn "%s" APIConnection.Connection.SQL.Config.ConnectString
        let sqlConnection = APIConnection.Connection.SQL.connection
        let command = new MySqlCommand("SELECT * FROM Employees;", sqlConnection)
        let reader = command.ExecuteReader()
        while reader.Read() do
            printfn "%A" (reader.GetString(0))
        [|1;2;3;4|]
