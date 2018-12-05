// Learn more about F# at http://fsharp.org

open System
open System.IO
[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let lines = File.ReadLines("../input")
    let sum = Seq.sumBy (fun x-> (int x)) (lines |> Seq.filter(fun x-> x <> String.Empty))
    printfn "Frequency shift: %d" sum
    0 // return an integer exit code
    