namespace UserAPI.Services

open UserAPI.Services
open UserAPI.Connector
open UserAPI.Models.SqlModel

type EmployeeService() = 
  inherit SqlService<Employee>(APIConnection.Connection.SQL.SqlData.Employees)

  member this.Login (username: string) (password: string) = 
    let result = this.SelectWithFilter $"WHERE username=%s{username} AND password=%s{password}" null
    if result.Count > 0 then Some(result[0]) else None

  member this.Register (username: string) (password: string) (email: string) = 
    let check = this.SelectWithFilter $"WHERE username=%s{username} AND password=%s{password}" null
    if check.Count = 0 then
      let result = this.InsertSingle "username, password, email" $"%s{username}, %s{password}, %s{email}"
      if result.Count > 0 then Some(result[0]) else None
    else
      None

  member this.SelectEmployeeById (employeeId: string) (fileds: string) = 
    let result = ($"WHERE id=%s{employeeId}", fileds) ||> this.SelectWithFilter
    if result.Count > 0 then Some(result[0]) else None
