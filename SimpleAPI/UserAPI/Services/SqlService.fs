namespace UserAPI.Services

open System.Collections.Generic
open UserAPI.Models.SqlModel.DataSet

type SqlService<'T when 'T: (new: unit -> 'T)>(dataSet: SqlDataSet<'T>) =
    member this.dataSet = dataSet

    member this.SelectAll (projects: string): List<'T> =
        if projects = null then
            this.dataSet.SelectAll "*"
        else
            this.dataSet.SelectAll projects

    member this.SelectWithFilter (filterCommand: string) (projects: string): List<'T> =
        let rawData =
            if projects = null then
                this.dataSet.SelectWithFilter filterCommand "*"
            else
                this.dataSet.SelectWithFilter filterCommand projects
        rawData

    member this.InsertSingle (insertedElement: string) (insertedValue: string) =
        this.dataSet.InsertSingle insertedElement insertedValue
