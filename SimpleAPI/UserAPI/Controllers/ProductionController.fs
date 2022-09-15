namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector
open UserAPI.Models.SqlModel

[<ApiController>]
type ProductionController =
    inherit ControllerBase

    val logger: ILogger<ProductionController>
    val productionModel: DataSet.SqlDataSet<Production>

    new(logger: ILogger<ProductionController>) =
        { inherit ControllerBase()
          logger = logger
          productionModel = APIConnection.Connection.SQL.SqlData.Productions }

    [<HttpGet("/production-list")>]
    member this.GetListProductions() =
        let productions = APIConnection.Connection.SQL.SqlData.Productions
        productions.SelectAll()
