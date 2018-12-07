open System
open System.IO

// Compute frequency shift using F# sequences
let part1 file = 
    printfn "Computing frequency shift... or something..."
    let lines = File.ReadLines(file)
    let sum = Seq.sumBy (fun x-> (int x)) (lines |> Seq.filter(fun x-> x <> String.Empty))
    printfn "Frequency shift: %d" sum

// Sequences are lazy, so we can use them to repeat a given list infinitely
let rec repeat items = seq {
    yield! items 
    yield! repeat items}

// Tuples are annyoing to use in F# with more than 2 elements,
// so we define a type to capture the state for our sequence 
// operations in part2 of the puzzle
// we need to have set to check if a frequence was already seen
// we store if the current set had seen the current frequency already
// and we keep track of the current frequency sum
type Collector = { set : Set<int>; contained : bool; sum : int}

let part2 file=
    printfn "Computing phase alignment..."
    // parse numbers and build infinite sequence that just repeats them
    let numbers = repeat (File.ReadLines(file) |> Seq.filter(fun x->x <> String.Empty) |> Seq.map(fun x->(int x)))
    // first sum the sequence to get absolute frequencies form the shifts
    // then we iterate over the sequencye and stick every element into a set to check if we see one twice
    let seq = numbers |> Seq.scan (+) 0
                      |> Seq.scan (fun col x -> {set=col.set.Add(x); contained=Set.contains x col.set; sum=x}) {set=Set.empty; contained = false; sum=0}
                      |> Seq.skipWhile (fun col -> not col.contained)   // skip all the elements until we find the first occurence of a repeated frequecny
                      |> Seq.take 1 // take that element and get the frequency
                      |> Seq.map (fun col -> col.sum)
    seq |> Seq.iter (fun x-> printfn "Phase alignment: %d" x)

[<EntryPoint>]
let main argv =
    printfn "Advent of Code -- Day 01"
    part1 "../input"
    part2 "../input2"
    0