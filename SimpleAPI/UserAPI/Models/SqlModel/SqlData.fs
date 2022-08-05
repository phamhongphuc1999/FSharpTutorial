namespace UserAPI.Models.SqlModel

open MySqlConnector
open UserAPI.Models.SqlModel.DataSet

[<AllowNullLiteral>]
type SqlData private () =
    let mutable connection: MySqlConnection = null
    let mutable employees: SqlDataSet<Employee> = null
    let mutable productions: SqlDataSet<Production> = null
    let mutable bills: SqlDataSet<Bill> = null

    member this.Connection
        with get () = connection
        and private set value = connection <- value

    member this.Employees
        with get () = employees
        and private set value = employees <- value

    member this.Productions
        with get () = productions
        and private set value = productions <- value

    member this.Bills
        with get () = bills
        and private set value = bills <- value

    new(connection: MySqlConnection) as this =
        SqlData()
        then
            this.Connection <- connection
            this.Employees <- new SqlDataSet<Employee>(connection)
            this.Productions <- new SqlDataSet<Production>(connection)
            this.Bills <- new SqlDataSet<Bill>(connection)
