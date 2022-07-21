namespace MyNumber.Vector

open MyNumber.Number.DecimalNumber

module ThreeDirectionVector =
    type ThreeDirectionVector(x: DecimalNumber, y: DecimalNumber, z: DecimalNumber) =
        let mutable x = x
        let mutable y = y
        let mutable z = z

        member this.X
            with get () = x
            and set value = x <- value

        member this.Y
            with get () = y
            and set value = y <- value

        member this.Z
            with get () = z
            and set value = z <- value

        member this.IsEqual(vector: ThreeDirectionVector) =
            (this.X = vector.X)
            && (this.Y = vector.Y)
            && (this.Z = vector.Z)

        static member op_Equality(vector1: ThreeDirectionVector, vector2: ThreeDirectionVector) =
            vector1.IsEqual vector2

        member this.Add(vector: ThreeDirectionVector) =
            (this.X + vector.X, this.Y + vector.Y, this.Z + vector.Z)
            |> ThreeDirectionVector

        static member (+)(vector1: ThreeDirectionVector, vector2: ThreeDirectionVector) = vector1.Add(vector2)

        member this.Subtract(vector: ThreeDirectionVector) =
            (this.X - vector.X, this.Y - vector.Y, this.Z - vector.Z)
            |> ThreeDirectionVector

        static member (-)(vector1: ThreeDirectionVector, vector2: ThreeDirectionVector) = vector1.Subtract(vector2)

        member this.SlaceMultiple(number: DecimalNumber) =
            (this.X * number, this.Y * number, this.Z * number)
            |> ThreeDirectionVector

        static member (.*)(vector1: ThreeDirectionVector, number: DecimalNumber) = vector1.SlaceMultiple number

        member this.Multiple(vector: ThreeDirectionVector) =
            ((this.X * vector.X)
             + (this.Y * vector.Y)
             + (this.Z * vector.Z))

        static member (*)(vector1: ThreeDirectionVector, vector2: ThreeDirectionVector) = vector1.Multiple vector2
