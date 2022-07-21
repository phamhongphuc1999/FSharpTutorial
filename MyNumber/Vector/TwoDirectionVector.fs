namespace MyNumber.Vector

open MyNumber.Number.DecimalNumber

module TwoDirectionVector =
    type TwoDirectionVector(x: DecimalNumber, y: DecimalNumber) =
        let mutable x = x
        let mutable y = y

        member this.X
            with get () = x
            and set value = x <- value

        member this.Y
            with get () = y
            and set value = y <- value

        member this.IsEqual(vector: TwoDirectionVector) =
            (this.X = vector.X) && (this.Y = vector.Y)

        static member op_Equality(vector1: TwoDirectionVector, vector2: TwoDirectionVector) = vector1.IsEqual vector2

        member this.Add(vector: TwoDirectionVector) =
            (this.X + vector.X, this.Y + vector.Y)
            |> TwoDirectionVector

        static member (+)(vector1: TwoDirectionVector, vector2: TwoDirectionVector) = vector1.Add(vector2)

        member this.Subtract(vector: TwoDirectionVector) =
            (this.X - vector.X, this.Y - vector.Y)
            |> TwoDirectionVector

        static member (-)(vector1: TwoDirectionVector, vector2: TwoDirectionVector) = vector1.Subtract(vector2)

        member this.SlaceMultiple(number: DecimalNumber) =
            (this.X * number, this.Y * number)
            |> TwoDirectionVector

        static member (.*)(vector1: TwoDirectionVector, number: DecimalNumber) = vector1.SlaceMultiple number

        member this.Multiple(vector: TwoDirectionVector) =
            ((this.X * vector.X) + (this.Y * vector.Y))

        static member (*)(vector1: TwoDirectionVector, vector2: TwoDirectionVector) = vector1.Multiple vector2
