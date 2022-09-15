namespace UserAPI.Models.SqlModel.DataSet

open MySqlConnector
open System.Collections.Generic

[<AllowNullLiteral>]
type SqlDataSet<'T when 'T: (new: unit -> 'T)>(connection: MySqlConnection) =
    inherit BaseSqlDataSet<'T>(connection)

    member this.SelectAll() =
        let command =
            new MySqlCommand($"SELECT * FROM %s{this.TableName};", this.Connection)

        let reader = command.ExecuteReader()
        let result = new List<'T>()

        while (reader.Read()) do
            let _item = this.ConvertDataSetToObject(reader)
            result.Add(_item)

        reader.Close()
        result

    member this.SelectWithFilter(filterCommand: string) =
        let command =
            new MySqlCommand($"SELECT * FROM %s{this.TableName} %s{filterCommand};", this.Connection)

        let reader = command.ExecuteReader()
        let result = new List<'T>()

        while (reader.Read()) do
            let _item = this.ConvertDataSetToObject(reader)
            result.Add(_item)

        reader.Close()
        result
