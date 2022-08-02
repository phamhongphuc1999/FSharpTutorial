namespace UserAPI.Connector

open Microsoft.Extensions.Configuration
open UserAPI.Configuration
open MySqlConnector
open UserAPI.Models.SqlModel

[<AllowNullLiteral>]
type ApiSqlConnector private () =
    static let mutable connector: ApiSqlConnector = null
    let mutable config: MySqlConfig = null
    let mutable connection: MySqlConnection = null
    let mutable sqlData: SqlData = null

    static member Connector
        with get () = connector
        and private set value = connector <- value

    member this.Config
        with get () = config
        and private set value = config <- value

    member this.Connection
        with get () = connection
        and private set value = connection <- value

    member this.SqlData with get() = sqlData and private set value = sqlData <- value

    private new(configuration: IConfigurationSection) as this =
        ApiSqlConnector()
        then
            this.Config <- new MySqlConfig()
            configuration.Bind(this.Config)
            this.Connection <- new MySqlConnection(this.Config.ConnectString)
            this.Connection.Open()
            this.SqlData <- new SqlData(this.Connection)

    static member GetInstance(configuration: IConfigurationSection) =
        match ApiSqlConnector.Connector with
        | null -> ApiSqlConnector.Connector <- configuration |> ApiSqlConnector
        | _ -> ()

        ApiSqlConnector.Connector
