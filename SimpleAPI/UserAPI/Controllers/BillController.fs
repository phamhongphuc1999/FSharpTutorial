namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector

open UserAPI.Models.SqlModel

[<ApiController>]
type BillController =
    inherit ControllerBase

    val logger: ILogger<BillController>
    val billModel: DataSet.SqlDataSet<Bill>

    new(logger: ILogger<BillController>) =
        { inherit ControllerBase()
          logger = logger
          billModel = APIConnection.Connection.SQL.SqlData.Bills }

    [<HttpGet("/bill-list")>]
    member this.GetListBills() =
        let bills = APIConnection.Connection.SQL.SqlData.Bills
        bills.SelectAll()

    [<HttpGet("/bill/{billId}")>]
    member this.getBill(billId: string) = ()
