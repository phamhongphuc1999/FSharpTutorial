namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector
open UserAPI.Services
open UserAPI.Models.SqlModel

[<ApiController>]
type ProductionController =
    inherit ControllerBase

    val logger: ILogger<ProductionController>
    val productionService: SqlService<Production>

    new(logger: ILogger<ProductionController>) =
        { inherit ControllerBase()
          logger = logger
          productionService = new SqlService<Production>(APIConnection.Connection.SQL.SqlData.Productions) }

    [<HttpGet("/production-list")>]
    member this.GetListProductions() = this.productionService.SelectAll()

    [<HttpGet("/production/{productionId}")>]
    member this.GetProductionById (productionId: string) ([<FromQuery>] fileds: string) =
        this.productionService.SelectWithFilter $"WHERE Id=%s{productionId}" fileds
