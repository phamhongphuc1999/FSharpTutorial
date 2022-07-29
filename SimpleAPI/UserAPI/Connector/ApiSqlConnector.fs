namespace UserAPI.Connector

open Microsoft.Extensions.Configuration
open UserAPI.Configuration
open MySqlConnector

[<AllowNullLiteral>]
type ApiSqlConnector private () =
    static let mutable connector: ApiSqlConnector = null
    static member Connector with get() = connector and set value = connector <- value
    member val Config: MySqlConfig = null with get, set
    member val connection: MySqlConnection = null with get, set

    private new (configuration: IConfigurationSection) as this =
        ApiSqlConnector() then
            this.Config <- new MySqlConfig()
            configuration.Bind(this.Config)
            this.connection <- new MySqlConnection(this.Config.ConnectString)
            this.connection.Open()

    static member GetInstance(configuration: IConfigurationSection) =
        match ApiSqlConnector.Connector with
        | null -> ApiSqlConnector.Connector <- configuration |> ApiSqlConnector
        | _ -> ()
        ApiSqlConnector.Connector
