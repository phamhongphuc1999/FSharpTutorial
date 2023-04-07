namespace UserAPI

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open UserAPI.Services

type Startup private () =
    member val Configuration: IConfiguration = null with get, set
    member val _env: IWebHostEnvironment = null with get, set

    new(configuration: IConfiguration, env: IWebHostEnvironment) as this =
        Startup()
        then
            this.Configuration <- configuration
            this._env <- env

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =
        // Add swagger service
        services.AddSwaggerGen(fun options -> options.IncludeXmlComments("Properties/UserAPI.xml"))
        |> ignore

        // Add Cors service
        services.AddCors (fun options ->
            options.AddPolicy(
                "MyCorsPolicy",
                fun builder ->
                    builder
                        .WithOrigins("http://localhost:5000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                    |> ignore
            ))
        |> ignore

        // Add framework services.
        services.AddControllers() |> ignore
        services.AddHttpContextAccessor() |> ignore
        services.AddMvcCore() |> ignore

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore
        elif (env.IsProduction()) then
            app.UseExceptionHandler() |> ignore

        // Swagger for development
        app.UseSwagger(fun options -> options.SerializeAsV2 <- true) |> ignore

        app.UseSwaggerUI (fun options ->
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1")
            options.RoutePrefix <- "")
        |> ignore

        app.UseRouting() |> ignore
        app.UseAuthorization() |> ignore

        app.UseCors("MyCorsPolicy") |> ignore

        let baseUrl = this.Configuration.GetValue<string>("Develop:ApplicationUrl")
        app.UseMiddleware<LoggerMiddleware>(baseUrl) |> ignore

        app.UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)
        |> ignore
