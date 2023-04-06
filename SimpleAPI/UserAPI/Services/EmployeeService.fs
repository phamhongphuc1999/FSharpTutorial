namespace UserAPI.Services

open UserAPI.Services
open UserAPI.Connector
open UserAPI.Models.SqlModel

type EmployeeService() = 
  inherit SqlService<Employee>(APIConnection.Connection.SQL.SqlData.Employees)

  member this.Login (username: string) (password: string) = 
    $"WHERE username=%s{username} AND password=%s{password}" |> this.SelectWithFilter

  member this.SelectEmployeeById (employeeId: string) (fileds: string) = 
    ($"WHERE id=%s{employeeId}", fileds) ||> this.SelectWithFilter
