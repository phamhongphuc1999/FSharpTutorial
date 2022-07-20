namespace MyLibrary.MyCollection.LinkingCollection

module Double =
    [<AllowNullLiteral>]
    type DoubleNode<'T>(data: 'T) =
        let mutable _data = data
        let mutable _next: DoubleNode<'T> = null
        let mutable _prev: DoubleNode<'T> = null

        member this.Data
            with get () = _data
            and set value = _data <- value

        member this.Next
            with get () = _next
            and set value = _next <- value

        member this.Prev
            with get () = _prev
            and set value = _prev <- value

        override this.ToString() = this.Data.ToString()

    type DoubleLinkedList =
        class
        end
