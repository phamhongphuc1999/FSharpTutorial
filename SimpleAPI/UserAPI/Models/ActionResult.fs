namespace UserAPI.Models

type ActionResult<'T>(data: 'T, statusCode: int) =
  let mutable data = data
  let mutable statusCode = statusCode
  let mutable message: string = ""

  new (data: 'T, statusCode: int, _message: string) as this =
    this.Message <- _message
    ActionResult(data, statusCode)


  member this.Data with get() = data and private set value = data <- value
  member this.StatusCode with get() = statusCode and private set value = statusCode <- value
  member this.Message with get() = message and private set value = message <- value
