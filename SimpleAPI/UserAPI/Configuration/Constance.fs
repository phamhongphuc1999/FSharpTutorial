namespace UserAPI.Configuration

open System.Collections.Generic

type TableInfo =
    { Name: string
      PropertyType: string
      AttributeName: string }

type FullData =
    { ModuleName: string
      ModuleInfo: List<TableInfo> }

type TableSchema = { Name: string; Type: string }

module Config =
  let EmployeeElement = [|"id"; "username"; "password"; "email"|]
  let ProductionElement = [|"id"; "name"; "amount"; "employeeId"|]
