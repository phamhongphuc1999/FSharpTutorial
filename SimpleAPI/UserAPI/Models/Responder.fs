namespace UserAPI.Models

type ResponseSuccessType() =

  member val status: string = "" with get, set
  member val data: obj = null with get, set

type ResponseFailType() =
  member val status: string = "" with get, set
  member val reason: obj = null with get, set
