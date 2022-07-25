namespace MyNumber.Vector

open MyNumber.Vector.TwoDirectionVector
open MyNumber.Vector.ThreeDirectionVector
open MyNumber.Number.DecimalNumber
open MyNumber.Number.UIntNumber
open MyNumber.Error
open System

module NDirectionVector =
    type NDirectionVector(location: DecimalNumber []) =
        let mutable location = location

        do
            if (location.Length = 0) then
                raise (Exception("Direction is greater than 0"))

        member this.Location
            with get () = location
            and set value = location <- value

        member this.Direction = location.Length

        member this.IsEqual(vector: NDirectionVector) =
            if this.Direction <> vector.Direction then
                raise (NotSameDirection("Not Same Direction"))

            let mutable check = true
            let mutable count = 0

            while check && count < this.Direction do
                if this.Location[count] <> vector.Location[count] then
                    check <- false

                count <- count + 1

            check

        static member op_Equality(vector1: NDirectionVector, vector2: NDirectionVector) = vector1.IsEqual vector2

        member this.Length() = ()
        // let twoNumber = UIntNumber "2"
        // let mutable result = DecimalNumber "0"
        // let mutable count = 0

        // while count < this.Direction do
        //     result <- result + (this.Location[count] .^ twoNumber)
        //     count <- count + 1

        // result ^^ twoNumber

        member this.Add(vector: NDirectionVector) =
            if this.Direction <> vector.Direction then
                raise (NotSameDirection("Not Same Direction"))

            let result = ResizeArray<DecimalNumber>()
            let mutable count = 0

            while count < this.Direction do
                result.Add(this.Location[count] + vector.Location[count])
                count <- count + 1

            (result.ToArray()) |> NDirectionVector

        static member (+)(vector1: NDirectionVector, vector2: NDirectionVector) = vector1.Add(vector2)

        member this.Subtract(vector: NDirectionVector) =
            if this.Direction <> vector.Direction then
                raise (NotSameDirection("Not Same Direction"))

            let result = ResizeArray<DecimalNumber>()
            let mutable count = 0

            while count < this.Direction do
                result.Add(this.Location[count] - vector.Location[count])
                count <- count + 1

            (result.ToArray()) |> NDirectionVector

        static member (-)(vector1: NDirectionVector, vector2: NDirectionVector) = vector1.Subtract(vector2)

        member this.SlaceMultiple(number: DecimalNumber) =
            let result = ResizeArray<DecimalNumber>()
            let mutable count = 0

            while count < this.Direction do
                result.Add(number * this.Location[count])
                count <- count + 1

            (result.ToArray()) |> NDirectionVector

        static member (.*)(vector1: NDirectionVector, number: DecimalNumber) = vector1.SlaceMultiple number

        member this.Multiple(vector: NDirectionVector) =
            if this.Direction <> vector.Direction then
                raise (NotSameDirection("Not Same Direction"))

            let mutable result = DecimalNumber "0"
            let mutable count = 0

            while count < this.Direction do
                result <-
                    result
                    + (this.Location[count] * vector.Location[count])

                count <- count + 1

            result

        static member (*)(vector1: NDirectionVector, vector2: NDirectionVector) = vector1.Multiple vector2

        member this.TwoDirection() =
            match this.Direction with
            | 1 ->
                (this.Location[0], DecimalNumber "0")
                |> TwoDirectionVector
            | _ ->
                (this.Location[0], this.Location[1])
                |> TwoDirectionVector

        member this.ThreeDirection() =
            match this.Direction with
            | 1 ->
                (this.Location[0], DecimalNumber "0", DecimalNumber "0")
                |> ThreeDirectionVector
            | 2 ->
                (this.Location[0], this.Location[1], DecimalNumber "0")
                |> ThreeDirectionVector
            | _ ->
                (this.Location[0], this.Location[1], this.Location[2])
                |> ThreeDirectionVector
