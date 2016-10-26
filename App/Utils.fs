namespace App

[<Measure>]
type ms

type WebClientWithTimeout(timeout : int<ms>) = 
    inherit System.Net.WebClient()
    override __.GetWebRequest(address) = 
        let result = base.GetWebRequest(address)
        result.Timeout <- int timeout
        result

module Timer =
    let time countN label f  = 

        let stopwatch = System.Diagnostics.Stopwatch()
    
        System.GC.Collect()  

        printfn "======================="         
        printfn "%s" label 
        printfn "======================="         
    
        let mutable totalMs = 0L

        for iteration in [1..countN] do
            stopwatch.Restart() 
            f()
            stopwatch.Stop() 
            printfn "#%2i elapsed:%6ims " iteration stopwatch.ElapsedMilliseconds 
            totalMs <- totalMs + stopwatch.ElapsedMilliseconds

        let avgTimePerRun = totalMs / int64 countN
        printfn "%s: Average time per run:%6ims " label avgTimePerRun 
