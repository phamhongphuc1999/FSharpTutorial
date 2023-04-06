namespace MyLibrary.Sorting

module Sort =
    type PivotType =
        | HEADER = 0
        | END = 1
        | MEDIUM = 2

    let BubbleSort<'T> (source: 'T []) (compare: 'T -> 'T -> bool) =
        let tempArr = source |> Array.copy
        let length = tempArr.Length

        for i = 0 to length - 2 do
            for j = i + 1 to length - 1 do
                if not (compare tempArr.[i] tempArr.[j]) then
                    let temp = tempArr.[i]
                    tempArr.[i] <- tempArr.[j]
                    tempArr.[j] <- temp

        tempArr

    let InsertSort<'T> (source: 'T []) (compare: 'T -> 'T -> bool) =
        let tempArr = source |> Array.copy
        let length = tempArr.Length

        for i = 1 to length - 1 do
            let value = tempArr.[i]
            let mutable index = i - 1

            while index >= 0 && (compare value tempArr.[index]) do
                tempArr.[index + 1] <- tempArr.[index]
                index <- index - 1

            tempArr.[index] <- value

        tempArr

    let SelectSort<'T> (source: 'T []) (compare: 'T -> 'T -> bool) =
        let tempArr = source |> Array.copy
        let length = tempArr.Length

        for i = 0 to length - 2 do
            let mutable key = i

            for j = i + 1 to length - 1 do
                if (compare tempArr.[j] tempArr.[i]) then
                    key <- j

            if key <> i then
                let temp = tempArr.[i]
                tempArr.[i] <- tempArr.[key]
                tempArr.[key] <- temp

        tempArr

    let private Partition<'T>
        (source: 'T [])
        (beginPivot: int)
        (endPivot: int)
        (pivot: int)
        (compare: 'T -> 'T -> bool)
        =
        let mutable low = beginPivot
        let mutable hight = endPivot
        let value = source.[pivot]

        let mutable result = 0
        let mutable check = true

        while check do
            while (compare source.[low] value) do
                low <- low + 1

            while (compare value source.[hight]) do
                hight <- hight - 1

            if low < hight then
                let temp = source.[low]
                source.[low] <- source.[hight]
                source.[hight] <- temp
            else
                result <- hight
                check <- false

        result

    let rec private QuickListSort<'T>
        (source: 'T [])
        (beginPivot: int)
        (endPivot: int)
        (pivotType: PivotType)
        (compare: 'T -> 'T -> bool)
        =
        if beginPivot < endPivot then
            let mutable partition = 0

            if pivotType = PivotType.HEADER then
                partition <- Partition source beginPivot endPivot beginPivot compare
            elif pivotType = PivotType.END then
                partition <- Partition source beginPivot endPivot endPivot compare
            else
                partition <- Partition source beginPivot endPivot ((beginPivot + endPivot) / 2) compare

            QuickListSort source beginPivot partition pivotType compare
            QuickListSort source (partition + 1) endPivot pivotType compare

    let QuickSort<'T> (source: 'T []) (pivotType: PivotType) (compare: 'T -> 'T -> bool) =
        let tempArr = source |> Array.copy
        let length = tempArr.Length

        QuickListSort tempArr 0 (length - 1) pivotType compare

        tempArr

    let private Merge<'T> (source: 'T[]) (startIndex: int) (split: int) (endIndex: int) (comparer: 'T -> 'T -> bool) = 
        let lowerLen = split - startIndex + 1
        let upperLen = endIndex - split
        let (lowerArr: 'T array) = Array.zeroCreate lowerLen
        let (upperArr: 'T array) = Array.zeroCreate upperLen
        for i = 0 to lowerLen - 1 do
            lowerArr[i] <- source[startIndex + i]
        for i = 0 to upperLen - 1 do
            upperArr[i] <- source[split + 1 + i]
        let mutable lowerCounter = 0
        let mutable upperCounter = 0
        let mutable counter = startIndex
        while lowerCounter < lowerLen && upperCounter < upperLen do
            let lowerValue = lowerArr[lowerCounter]
            let upperValue = upperArr[upperCounter]
            if (comparer lowerValue upperValue) then
                source[counter] <- lowerValue
                lowerCounter <- lowerCounter + 1
            else
                source[counter] <- upperValue
                upperCounter <- upperCounter + 1
            counter <- counter + 1
        while lowerCounter < lowerLen do
            source[counter] <- lowerArr[lowerCounter]
            lowerCounter <- lowerCounter + 1
            counter <- counter + 1
        while upperCounter < upperLen do
            source[counter] <- upperArr[upperCounter]
            upperCounter <- upperCounter + 1
            counter <- counter + 1

    let rec private MergeListSort<'T> (source: 'T[]) (startIndex: int) (endIndex: int) (comparer: 'T -> 'T -> bool) = 
        if startIndex < endIndex then
            let split = (startIndex + endIndex) / 2
            MergeListSort source startIndex split comparer
            MergeListSort source (split + 1) endIndex comparer
            Merge source startIndex split endIndex comparer

    let MergeSort<'T> (source: 'T[]) (comparer: 'T -> 'T -> bool) = 
        let tempArr = source |> Array.copy
        let length = tempArr.Length
        MergeListSort tempArr 0 (length - 1) comparer
        tempArr
