namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Services

[<Produces("application/json")>]
[<Consumes("application/json")>]
[<ApiController>]
type ProductionController =
    inherit ControllerBase

    val logger: ILogger<ProductionController>
    val productionService: ProductionService

    new(logger: ILogger<ProductionController>) =
        { inherit ControllerBase()
          logger = logger
          productionService = new ProductionService() }

    [<HttpGet("/production-list")>]
    member this.GetListProductions() = this.productionService.SelectAll()

    [<HttpGet("/production/{productionId}")>]
    member this.GetProductionById (productionId: string) ([<FromQuery>] fileds: string) =
        this.productionService.GetProductionById productionId fileds
