namespace UserAPI.Services

open UserAPI.Models.SqlModel.DataSet

type SqlService<'T when 'T: (new: unit -> 'T)>(dataSet: SqlDataSet<'T>) =
    member this.dataSet = dataSet

    member this.SelectAll() = this.dataSet.SelectAll()

    member this.SelectWithFilter (filterCommand: string) (projects: string) =
        let rawData =
            if projects = null then
                this.dataSet.SelectWithFilter filterCommand "*"
            else
                this.dataSet.SelectWithFilter filterCommand projects
        rawData
