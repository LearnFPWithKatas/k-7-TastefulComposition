module App.Entry

open System
open App.Downloader

[<EntryPoint>]
let main _ = 
    "http://google.com"
    |> Uri
    |> getUriContent 
    |> Async.RunSynchronously 
    |> showContentResult 

    "http://example.bad"
    |> Uri
    |> getUriContent 
    |> Async.RunSynchronously 
    |> showContentResult 

    0
