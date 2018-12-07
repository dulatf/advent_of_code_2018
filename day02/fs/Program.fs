open System
open System.IO

// Count letter frequencies and check if any letter appears exacly 2 or 3 times
let processid idstring =
    let seq = idstring |> Seq.countBy id |> Seq.map (fun x-> (snd x))
    (Seq.contains 2 seq, Seq.contains 3 seq)

let computeChecksum file = 
    let lines = File.ReadLines(file)
    let seq = Seq.map processid lines
    let doubles = seq |> Seq.countBy (fst) |> Seq.filter (fst)
    let triples = seq |> Seq.countBy (snd) |> Seq.filter (fst)

    let doubleCount = doubles |> Seq.map (snd) |> Seq.exactlyOne
    let tripleCount = triples |> Seq.map (snd) |> Seq.exactlyOne
    (doubleCount, tripleCount, doubleCount*tripleCount)
[<EntryPoint>]
let main argv =
    printfn "Computing package ID checksum..."
    let (doubles, triples, checksum) = computeChecksum("../input")
    printfn "Doubles: %d\nTriples: %d\nChecksum: %d" doubles triples checksum
    0 // return an integer exit code
