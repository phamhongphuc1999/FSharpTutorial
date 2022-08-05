namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector

[<ApiController>]
[<Route("/bill")>]
type BillController(logger: ILogger<BillController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetListBills() =
        let employees = APIConnection.Connection.SQL.SqlData.Bills
        employees.SelectAll()
