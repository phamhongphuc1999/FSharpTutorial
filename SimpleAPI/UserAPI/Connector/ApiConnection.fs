namespace UserAPI.Connector

open Microsoft.Extensions.Configuration

[<AllowNullLiteral>]
type APIConnection private () =
    static let mutable connection: APIConnection = null
    member val SQL: ApiSqlConnector = null with get, set

    static member Connection with get() = connection and set value = connection <- value

    private new (sqlConfig: IConfigurationSection) as this =
        APIConnection() then
            this.SQL <- ApiSqlConnector.GetInstance sqlConfig

    static member GetConnection(sqlConfig: IConfigurationSection) =
        match APIConnection.Connection with
        | null -> APIConnection.Connection <- sqlConfig |> APIConnection
        | _ -> ()
        APIConnection.Connection
        