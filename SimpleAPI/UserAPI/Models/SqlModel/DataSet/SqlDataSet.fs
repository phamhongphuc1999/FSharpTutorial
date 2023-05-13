namespace UserAPI.Models.SqlModel.DataSet

open MySqlConnector
open System.Collections.Generic

[<AllowNullLiteral>]
type SqlDataSet<'T when 'T: (new: unit -> 'T)>(connection: MySqlConnection) =
    inherit BaseSqlDataSet<'T>(connection)

    member this.Execute (executedCommand: MySqlCommand) =
        let reader = executedCommand.ExecuteReader()
        let result = new List<'T>()

        try
            while (reader.Read()) do
                let _item = this.ConvertDataSetToObject(reader)
                result.Add(_item)
        finally
            reader.Close()

        result

    member this.SelectAll (projectCommand: string) =
        let command =
            new MySqlCommand($"SELECT %s{projectCommand} FROM %s{this.TableName};", this.Connection)

        command |> this.Execute

    member this.SelectWithFilter (filterCommand: string) (projectCommand: string) =
        let command =
            new MySqlCommand($"SELECT %s{projectCommand} FROM %s{this.TableName} %s{filterCommand};", this.Connection)
        command |> this.Execute

    member this.InsertSingle (insertedElement: string) (insertedValue: string) =
        let command = new MySqlCommand($"INSERT INTO %s{this.TableName} (%s{insertedElement}) VALUES (%s{insertedValue});")
        command |> this.Execute
