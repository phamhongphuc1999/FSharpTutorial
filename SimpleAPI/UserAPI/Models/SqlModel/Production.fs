namespace UserAPI.Models.SqlModel

open UserAPI

[<Table("Productions")>]
type Production() =
    member val NameProduction: string = "" with get, set
    member val Amount: int32 = 0 with get, set
    member val Status: int32 = 0 with get, set
