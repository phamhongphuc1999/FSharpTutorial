namespace UserAPI.Models.SqlModel

open MySqlConnector
open UserAPI.Models.SqlModel.DataSet

[<AllowNullLiteral>]
type SqlData private () =
    let mutable connection: MySqlConnection = null
    let mutable employees: SqlDataSet<Employee> = null

    member this.Connection
        with get () = connection
        and private set value = connection <- value

    member this.Employees
        with get () = employees
        and private set value = employees <- value

    new(connection: MySqlConnection) as this =
        SqlData()
        then
            this.Connection <- connection
            this.Employees <- new SqlDataSet<Employee>(connection)
