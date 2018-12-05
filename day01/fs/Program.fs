open System
open System.IO
[<EntryPoint>]
let main argv =
    printfn "Advent of Code -- Day 01"
    printfn "Computing frequency shift... or something..."
    let lines = File.ReadLines("../input")
    let sum = Seq.sumBy (fun x-> (int x)) (lines |> Seq.filter(fun x-> x <> String.Empty))
    printfn "Frequency shift: %d" sum
    0