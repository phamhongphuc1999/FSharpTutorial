namespace UserAPI.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open UserAPI.Connector

[<ApiController>]
[<Route("/production")>]
type ProductionController(logger: ILogger<ProductionController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetListProductions() =
        let employees = APIConnection.Connection.SQL.SqlData.Productions
        employees.SelectAll()
