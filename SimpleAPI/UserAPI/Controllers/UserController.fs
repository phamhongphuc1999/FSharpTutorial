namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector
open MySqlConnector
open System.Data

[<ApiController>]
[<Route("/user")>]
type UserController(logger: ILogger<UserController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetUser() =
        let sqlConnection = APIConnection.Connection.SQL.Connection
        let command = new MySqlCommand("select COLUMN_NAME, DATA_TYPE as 'name' from information_schema.columns where table_name=@t_name;", sqlConnection)
        command.Parameters.AddWithValue("@t_name", "Employees") |> ignore
        let mutable data = new DataTable()
        let da = new MySqlDataAdapter(command)
        da.Fill(data) |> ignore
        for _row in data.Rows do
            printfn "%A, %A" (_row.Item(0).ToString()) (_row.Item(1).ToString())

        //let command = new MySqlCommand("SELECT * FROM Employees;", sqlConnection)
        //let reader = command.ExecuteReader()
        //while reader.Read() do
        //    printfn "%s" (reader.GetValue("email") :?> string)
        //[|1;2;3;4|]
