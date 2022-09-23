namespace UserAPI.Models.SqlModel.DataSet

open System
open UserAPI
open System.Collections.Generic
open MySqlConnector
open System.Data
open UserAPI.Configuration

[<AllowNullLiteral>]
type BaseSqlDataSet<'T when 'T: (new: unit -> 'T)> private () =
    let mutable connection: MySqlConnection = null
    let mutable tableName: string = ""
    let mutable tableInfo: List<TableInfo> = new List<TableInfo>()

    member this.Connection
        with get () = connection
        and private set value = connection <- value

    member this.TableName
        with get () = tableName
        and private set value = tableName <- value

    member this.TableInfo
        with get () = tableInfo
        and private set value = tableInfo <- value

    new(connection: MySqlConnection) as this =
        BaseSqlDataSet()
        then
            this.Connection <- connection
            let moduleInfo: FullData = this.GetReflection()
            let tableSchema: List<string> = this.GetTableSchema(moduleInfo.ModuleName)

            let result = new List<TableInfo>()

            for item in moduleInfo.ModuleInfo do
                let name =
                    if item.AttributeName = null then
                        item.Name
                    else
                        item.AttributeName

                let check = tableSchema.Exists(fun x -> x = name)

                if not check then
                    raise (Exception("134"))

                result.Add({ item with AttributeName = name })

            this.TableName <- moduleInfo.ModuleName
            this.TableInfo <- result


    member private this.GetReflection() =
        let moduleInfo = typeof<'T>
        let properties = moduleInfo.GetProperties()
        let result = new List<TableInfo>()

        for _pro in properties do
            let _proAttribute = Attribute.GetCustomAttributes(_pro)
            let mutable count = 0
            let mutable check = true
            let len = _proAttribute.Length

            while count < len && check do
                if (_proAttribute.[count] :? TableRow) then
                    let _proTableRow = _proAttribute.[count] :?> TableRow

                    result.Add(
                        { Name = _pro.Name
                          PropertyType = _pro.PropertyType.Name
                          AttributeName = _proTableRow.Name }
                    )

                    check <- false

                count <- count + 1

            if check then
                result.Add(
                    { Name = _pro.Name
                      PropertyType = _pro.PropertyType.Name
                      AttributeName = null }
                )

        let attrs = Attribute.GetCustomAttributes(moduleInfo)
        let mutable moduleName = moduleInfo.Name
        let mutable count = 0
        let mutable check = true
        let len = attrs.Length

        while count < len && check do
            if (attrs.[count] :? Table) then
                let a = attrs.[count] :?> Table
                moduleName <- a.Name

            count <- count + 1

        { FullData.ModuleName = moduleName
          FullData.ModuleInfo = result }

    member private this.GetTableSchema(tableName: string) =
        let command =
            new MySqlCommand(
                "select COLUMN_NAME, DATA_TYPE as 'name' from information_schema.columns where table_name=@t_name;",
                this.Connection
            )

        command.Parameters.AddWithValue("@t_name", tableName)
        |> ignore

        let mutable data = new DataTable()
        let da = new MySqlDataAdapter(command)
        da.Fill(data) |> ignore
        let result = new List<string>()

        for _row in data.Rows do
            result.Add(_row.Item(0).ToString())

        if result.Count = 0 then
            raise (Exception("123"))

        result

    member private this.SetObjectValue<'A>
        (data: MySqlDataReader)
        (tableName: string)
        (obj: 'T)
        (propertyName: string)
        =
        try
            let _value = data.GetFieldValue<'A>(tableName)

            obj
                .GetType()
                .GetProperty(propertyName)
                .SetValue(obj, _value)
        with
        | _ ->
            obj
                .GetType()
                .GetProperty(propertyName)
                .SetValue(obj, null)

    member this.ConvertDataSetToObject(data: MySqlDataReader) =
        let result = new 'T()

        for item in this.TableInfo do
            let name = item.Name
            let tableName = item.AttributeName
            let _type = item.PropertyType

            match _type with
            | "String" -> this.SetObjectValue<string> data tableName result name
            | "Int16" -> this.SetObjectValue<int16> data tableName result name
            | "Int32" -> this.SetObjectValue<int32> data tableName result name
            | _ -> this.SetObjectValue<string> data tableName result name

        result
