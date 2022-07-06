open MyLibrary.Sort

let abc = BubbleSort<int> [ 1; 2; 3; 4 ] (fun a b -> a > b)
printfn "%A" abc
