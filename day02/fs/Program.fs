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


let compareIds (id1:string) (id2:string) =
    Seq.map2 (( = )) id1 id2
    |> Seq.countBy id
    |> Seq.filter (fun x -> not (fst x))
    |> Seq.map snd
    |> Seq.exactlyOne

let cartesianProduct xs ys =
    //xs |> Seq.collect (fun x-> ys |> Seq.map (fun y-> x, y))
    [for x in xs do
        for y in ys do
            yield (x,y)]


let removeNonEqual (s1:string) (s2:string) =
    Seq.map2 (fun c1 c2 -> ((c1, c2), c1=c2)) s1 s2
    |> Seq.filter snd
    |> Seq.map (fst >> fst)
    |> String.Concat

let findSimilarIds file = 
    let lines = File.ReadLines(file) |> List.ofSeq
    cartesianProduct lines lines
    |> Seq.filter (fun (x,y) -> not (x = y))
    |> Seq.map (fun (x,y) -> ((x, y), compareIds x y))
    |> Seq.filter (fun ((x,y),c) -> c=1)
    |> Seq.map (fun ((x,y),_) -> removeNonEqual x y)
    

[<EntryPoint>]
let main argv =
    printfn "Computing package ID checksum..."
    let (doubles, triples, checksum) = computeChecksum("../input")
    printfn "Doubles: %d\nTriples: %d\nChecksum: %d" doubles triples checksum
    (compareIds "abcde" "axcye")  
        |> printfn "%O"
    
    printfn "Finding similar IDs..."
    findSimilarIds "../input"
    |> Seq.iter (fun x -> printfn "%s" x)
    0 // return an integer exit code
