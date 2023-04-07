namespace UserAPI.Services

open UserAPI.Services
open UserAPI.Connector
open UserAPI.Models.SqlModel

type ProductionService() = 
  inherit SqlService<Production>(APIConnection.Connection.SQL.SqlData.Productions)

  member this.GetProductionById (productionId: string) (fileds: string) = 
    let result = ($"WHERE Id=%s{productionId}", fileds) ||> this.SelectWithFilter
    if result.Count > 0 then Some(result[0]) else None
