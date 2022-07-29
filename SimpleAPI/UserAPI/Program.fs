namespace UserAPI

open Microsoft.Extensions.Configuration
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open UserAPI.Connector

module Program =
    let CreateHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webBuilder ->
                webBuilder.UseStartup<Startup>() |> ignore
            )

    let CreateDbConnection () =
        let config = ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build()
        let sqlConfig = config.GetSection("MySqlSetting")
        printfn "%s" (sqlConfig.GetValue<string>("ConnectString"))
        APIConnection.GetConnection(sqlConfig)

    [<EntryPoint>]
    let Main args =
        CreateDbConnection() |> ignore
        CreateHostBuilder(args).Build().Run()
        0