namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Services
open UserAPI.Models

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

    /// <summary>get user by id</summary>
    /// <remarks>get user by id</remarks>
    /// <param name="productionId">the id of production you want to get</param>
    /// <param name="fields">the specified fields you want to get</param>
    /// <returns></returns>
    /// <response code="200">return information of production with specified fields</response>
    /// <response code="400">if get mistake</response>
    [<HttpGet("/production/{productionId}")>]
    [<ProducesResponseType(200, Type = typeof<ResponseSuccessType>)>]
    [<ProducesResponseType(400, Type = typeof<ResponseFailType>)>]
    member this.GetProductionById (productionId: string) ([<FromQuery>] fileds: string) =
        this.productionService.GetProductionById productionId fileds

    [<HttpGet("/production-list")>]
    member this.GetListProductions([<FromQuery>] fileds: string) =
        this.productionService.SelectAll fileds
