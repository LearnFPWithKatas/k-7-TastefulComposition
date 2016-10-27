module App.Entry

open System
open App.Downloader

let goodSites = [
    "http://google.com"
    "http://bbc.co.uk"
    "http://fsharp.org"
    "http://microsoft.com"
]

let badSites = [
    "http://example.com/nopage"
    "http://bad.example.com"
    "http://verybad.example.com"
    "http://veryverybad.example.com"
]

[<EntryPoint>]
let main _ = 
    let f() = 
        largestPageSizeA goodSites
        |> Async.RunSynchronously 
        |> showContentSizeResult 
    Timer.time 2 "largestPageSizeA_Good" f

    let f() = 
        largestPageSizeA badSites
        |> Async.RunSynchronously 
        |> showContentSizeResult 
    Timer.time 2 "largestPageSizeA_Bad" f

    0
