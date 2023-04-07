namespace UserAPI.Services

open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging

type LoggerMiddleware = 
  val mutable _next: RequestDelegate
  val mutable _logger: ILogger
  val mutable baseUrl: string

  member this.LoggerMiddleware (next:  RequestDelegate) (loggerFactory: ILoggerFactory) (baseUrl: string) = 
    this.baseUrl <- baseUrl
    this._next <- next
    this._logger <- loggerFactory.CreateLogger<LoggerMiddleware>()

  member this.Invoke (httpContext: HttpContext) =
    async {
      this._next.Invoke httpContext |> ignore
      let statusCode = httpContext.Response.StatusCode
      let mutable query = ""
      for item in httpContext.Request.Query do
        query <- $"%s{query}%s{item.Key}=%s{item.Value.ToString()}"
      let mutable logString = ""
      let method = httpContext.Request.Method
      let path = httpContext.Request.Path.ToString()
      if query.Length > 0 then
        query <- query.Substring(0, query.Length - 1)
        logString <- $"%s{method}: %s{this.baseUrl}%s{path}?%s{query} => %i{statusCode}"
      else
        logString <- $"%s{method}: %s{this.baseUrl}%s{path} => %i{statusCode}"
      if statusCode >= 200 && statusCode < 300 then
        this._logger.LogInformation logString
      else
        this._logger.LogError logString
    } 