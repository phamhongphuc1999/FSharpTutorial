namespace UserAPI.Services

open UserAPI.Services
open UserAPI.Connector
open UserAPI.Models.SqlModel

type ProductionService() = 
  inherit SqlService<Production>(APIConnection.Connection.SQL.SqlData.Productions)

  member this.GetProductionById (productionId: string) (fileds: string) = 
    ($"WHERE Id=%s{productionId}", fileds) ||> this.SelectWithFilter
