namespace UserAPI.Configuration

[<AllowNullLiteral>]
type MySqlConfig() =
    member val ConnectString: string = "" with get, set
