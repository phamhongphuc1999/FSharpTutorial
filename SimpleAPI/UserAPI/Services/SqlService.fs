namespace UserAPI.Services

open UserAPI.Models.SqlModel.DataSet
open System.Collections.Generic

type SqlService<'T when 'T: (new: unit -> 'T)>(dataSet: SqlDataSet<'T>) =
    member this.dataSet = dataSet

    member this.SelectAll() = this.dataSet.SelectAll()

    member this.SelectWithFilter (filterCommand: string) (projectCommand: string) =
        let rawData =
            if projectCommand = null then
                this.dataSet.SelectWithFilter filterCommand "*"
            else
                this.dataSet.SelectWithFilter filterCommand projectCommand

        let result = new List<Dictionary<string, string>>()
        ()
