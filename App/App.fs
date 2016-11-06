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
        largestPageSizeM goodSites
        |> Async.RunSynchronously 
        |> showContentSizeResult 
    Timer.time 2 "largestPageSizeM_Good" f

    let f() = 
        largestPageSizeM badSites
        |> Async.RunSynchronously 
        |> showContentSizeResult 
    Timer.time 2 "largestPageSizeM_Bad" f

    0
