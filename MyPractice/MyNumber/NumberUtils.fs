namespace MyNumber

open MyNumber.Service.UInt
open MyNumber.Service.Decimal
open MyNumber.Error

module EUtils =
    let GetENumber (uintNum: string) (accuracy: int) =
        if not (IsUInt uintNum) then
            raise (NotANumber("Not UInt Number"))

        let mutable result = "2"
        let mutable count = "2"
        let mutable temp = "1"

        while (UIntCompare count uintNum) <= 0 do
            temp <- MultipliedUInt temp count

            result <-
                ((DivideDecimal "1" temp accuracy), result)
                ||> AddDecimal

            count <- AddUInt count "1"

        result

    let GetEPow (x: string) (uintNum: string) (accuracy: int) =
        if not (IsDecimal x) then
            raise (NotANumber("Not Int Number"))

        if not (IsUInt uintNum) then
            raise (NotANumber("Not UInt Number"))

        let mutable result = AddDecimal "1" x
        let mutable count = "2"
        let mutable temp = "1"
        let mutable xTemp = x

        while (UIntCompare count uintNum) <= 0 do
            temp <- MultipliedUInt temp count
            xTemp <- MultipliedDecimal xTemp x

            result <-
                ((DivideDecimal xTemp temp accuracy), result)
                ||> AddDecimal

            count <- AddUInt count "1"

        result

module LogaritUtils =
    let private GetTaylorLogarit (x: string) (uintNum: string) (accuracy: int) =
        let mutable realX = x
        let mutable result = "0"
        let mutable count = "1"

        while (UIntCompare count uintNum) <= 0 do
            if (DivideModUInt count "2") = "0" then
                result <- SubtractDecimal result (DivideDecimal realX count accuracy)
            else
                result <- AddDecimal result (DivideDecimal realX count accuracy)

            count <- AddUInt count "1"
            realX <- MultipliedDecimal realX x

        result

    let rec GetLogarit (x: string) (uintNum: string) (accuracy: int) =
        let realX = SubtractDecimal x "1"
        let check1 = DecimalCompare realX "-1"
        let check2 = DecimalCompare realX "1"

        if check1 = 1 && check2 <= 0 then
            (GetTaylorLogarit realX uintNum accuracy)
        else
            let temp = DivideDecimal "1" realX accuracy
            let temp1 = GetLogarit (AddDecimal temp "1") uintNum accuracy
            let temp2 = GetLogarit temp uintNum accuracy
            SubtractDecimal temp1 temp2
