namespace UserAPI.Models.SqlModel

open System
open UserAPI
open System.Collections.Generic
open MySqlConnector
open System.Data

type Data = {
    Name: string
    PropertyType: string
    AttributeName: string
}

type FullData = {
    ModuleName: string
    ModuleInfo: List<Data>
}

type SqlDataSet<'T> private () =
    let mutable connection: MySqlConnection = null
    member this.Connection with get() = connection and private set value = connection <- value

    new (connection: MySqlConnection) as this =
        SqlDataSet() then
            this.Connection <- connection

    member this.GetReflection() =
        let moduleInfo = typeof<'T>
        let properties = moduleInfo.GetProperties()
        let result = new List<Data>()
        for _pro in properties do
            let _proAttribute = Attribute.GetCustomAttributes(_pro)
            let mutable count = 0
            let mutable check = true
            let len = _proAttribute.Length
            while count < len && check do
                if (_proAttribute.[count] :? TableRow) then
                    let _proTableRow = _proAttribute.[count] :?> TableRow
                    result.Add({Name = _pro.Name; PropertyType = _pro.PropertyType.Name; AttributeName = _proTableRow.Name})
                    check <- false
                count <- count + 1
            if check then
                result.Add({Name = _pro.Name; PropertyType = _pro.PropertyType.Name; AttributeName = null})
        let attrs = Attribute.GetCustomAttributes(moduleInfo)
        let mutable moduleName = moduleInfo.Name
        let mutable count = 0
        let mutable check = true
        let len = attrs.Length
        while count < len && check do
            if (attrs.[count] :? Table) then
                let a = attrs.[count] :?> Table
                moduleName <- a.Name
        {ModuleName = moduleName; ModuleInfo = result}

    member this.GetTableSchema(tableName: string) =
        let command = new MySqlCommand("select COLUMN_NAME, DATA_TYPE as 'name' from information_schema.columns where table_name=@t_name;", this.Connection)
        command.Parameters.AddWithValue("@t_name", tableName) |> ignore
        let mutable data = new DataTable()
        let da = new MySqlDataAdapter(command)
        da.Fill(data) |> ignore
        data
