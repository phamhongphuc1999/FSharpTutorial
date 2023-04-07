namespace UserAPI.Services

open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging

type LoggerMiddleware private () = 
  let mutable _next: RequestDelegate = null
  let mutable _logger: ILogger = null
  let mutable baseUrl: string = null

  member this.Next with get() = _next and private set value = _next <- value
  member this.Logger with get() = _logger and private set value = _logger <- value
  member this.BaseUrl with get() = baseUrl and private set value = baseUrl <- value

  new (next:  RequestDelegate, loggerFactory: ILoggerFactory, baseUrl: string) as this = 
    LoggerMiddleware()
    then
      this.BaseUrl <- baseUrl
      this.Next <- next
      this.Logger <- loggerFactory.CreateLogger<LoggerMiddleware>()

  member this.Invoke (httpContext: HttpContext) =
    let task = this.Next.Invoke httpContext
    let statusCode = httpContext.Response.StatusCode
    let mutable query = ""
    for item in httpContext.Request.Query do
      query <- $"%s{query}%s{item.Key}=%s{item.Value.ToString()}"
    let mutable logString = ""
    let method = httpContext.Request.Method
    let path = httpContext.Request.Path.ToString()
    if query.Length > 0 then
      query <- query.Substring(0, query.Length - 1)
      logString <- $"%s{method}: %s{this.BaseUrl}%s{path}?%s{query} => %i{statusCode}"
    else
      logString <- $"%s{method}: %s{this.BaseUrl}%s{path} => %i{statusCode}"
    if statusCode >= 200 && statusCode < 300 then
      this.Logger.LogInformation logString
    else
      this.Logger.LogError logString
    task