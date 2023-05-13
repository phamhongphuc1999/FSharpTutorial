namespace MyLibrary.Sorting

module Heap =
  let LEFT (i: int) =
    2 * i + 1

  let RIGHT (i: int) =
    2 * i + 2

  let rec Heapfy<'T> (source: 'T[]) (index: int) (length: int) (comparer: 'T -> 'T -> bool) =
    let left = LEFT index
    let right = RIGHT index
    let mutable largest = index
    if left < length && (comparer source[left] source[index]) then
      largest <- left
    if right < length && (comparer source[right] source[largest]) then
      largest <- right
    if largest <> index then
      let temp = source[index]
      source[index] <- source[largest]
      source[largest] <- temp
      Heapfy source largest length comparer

  let BuildHeap<'T> (source: 'T[]) (length: int) (comparer: 'T -> 'T -> bool) =
    let value = source.Length / 2;
    for i = value downto 0 do
      Heapfy source i length comparer

  let HeapSort<'T> (source: 'T[]) (comparer: 'T -> 'T -> bool) =
    let tempArr = source |> Array.copy
    let length = tempArr.Length
    BuildHeap tempArr length comparer
    for i = length - 1 downto 1 do
      let temp = source[0]
      source[0] <- source[i]
      source[i] <- temp
      Heapfy tempArr 0 i comparer
