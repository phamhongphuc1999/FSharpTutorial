namespace MyLibrary

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
                if not (compare tempArr[i] tempArr[j]) then
                    let temp = tempArr[i]
                    tempArr[i] <- tempArr[j]
                    tempArr[j] <- temp

        tempArr

    let InsertSort<'T> (source: 'T []) (compare: 'T -> 'T -> bool) =
        let tempArr = source |> Array.copy
        let length = tempArr.Length

        for i = 1 to length - 1 do
            let value = tempArr[i]
            let mutable index = i - 1

            while index >= 0 && (compare value tempArr[index]) do
                tempArr[index + 1] <- tempArr[index]
                index <- index - 1

            tempArr[index] <- value

        tempArr

    let SelectSort<'T> (source: 'T []) (compare: 'T -> 'T -> bool) =
        let tempArr = source |> Array.copy
        let length = tempArr.Length

        for i = 0 to length - 2 do
            let mutable key = i

            for j = i + 1 to length - 1 do
                if (compare tempArr[j] tempArr[i]) then
                    key <- j

            if key <> i then
                let temp = tempArr[i]
                tempArr[i] <- tempArr[key]
                tempArr[key] <- temp

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
        let value = source[pivot]

        let mutable result = 0
        let mutable check = true

        while check do
            while (compare source[low] value) do
                low <- low + 1

            while (compare value source[hight]) do
                hight <- hight - 1

            if low < hight then
                let temp = source[low]
                source[low] <- source[hight]
                source[hight] <- temp
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
