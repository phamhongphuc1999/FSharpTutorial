open MyLibrary.Sort

let source = [| 10; 1; 9; 2; 8; 3; 7; 4; 6; 5 |]

let abc = QuickSort<int> source PivotType.HEADER (fun a b -> a < b)
printfn "%A" abc
