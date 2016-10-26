module App.Downloader

open System

type UriContent = 
    | UriContent of Uri * string

type UriContentSize = 
    | UriContentSize of Uri * int

let getUriContent (uri : Uri) = 
    async { 
        use client = new WebClientWithTimeout(1000<ms>)
        try 
            printfn "  [%s] Started ..." uri.Host
            let! html = client.AsyncDownloadString(uri)
            printfn "  [%s] ... finished" uri.Host
            let uriContent = UriContent(uri, html)
            return (Result.Success uriContent)
        with ex -> 
            printfn "  [%s] ... exception" uri.Host
            let err = sprintf "[%s] %A" uri.Host ex.Message
            return Result.Failure [ err ]
    }

let showContentResult result = 
    match result with
    | Success(UriContent(uri, html)) -> printfn "SUCCESS: [%s] First 100 chars: %s" uri.Host (html.Substring(0, 100))
    | Failure errs -> printfn "FAILURE: %A" errs
