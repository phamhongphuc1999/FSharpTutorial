namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector
open UserAPI.Services
open UserAPI.Models.SqlModel

[<ApiController>]
type BillController =
    inherit ControllerBase

    val logger: ILogger<BillController>
    val billService: SqlService<Bill>

    new(logger: ILogger<BillController>) =
        { inherit ControllerBase()
          logger = logger
          billService = new SqlService<Bill>(APIConnection.Connection.SQL.SqlData.Bills) }

    [<HttpGet("/bill-list")>]
    member this.GetListBills() = this.billService.SelectAll()

    [<HttpGet("/bill/{billId}")>]
    member this.GetBillById (billId: string) ([<FromQuery>] fileds: string) =
        this.billService.SelectWithFilter $"WHERE Id=%s{billId}" fileds
