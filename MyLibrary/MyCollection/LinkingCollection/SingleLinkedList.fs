namespace MyLibrary.MyCollection.LinkingCollection

open System.Collections

module Single =
    [<AllowNullLiteral>]
    type SingleNode<'T>(data: 'T) =
        let mutable _data = data
        let mutable _next: SingleNode<'T> = null

        member this.Data
            with get () = _data
            and set value = _data <- value

        member this.Next
            with get () = _next
            and set value = _next <- value

        override this.ToString() = this.Data.ToString()

    type SingleLinkedList<'T>(_size: int, _begin: SingleNode<'T>, _end: SingleNode<'T>) =
        let mutable _size = _size
        let mutable _begin: SingleNode<'T> = _begin
        let mutable _end: SingleNode<'T> = _end
