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

let showContentSizeResult = 
    function 
    | Success(UriContentSize(uri, len)) -> printfn "SUCCESS: [%s] Content size is %i" uri.Host len
    | Failure errs -> printfn "FAILURE: %A" errs

let makeContentSize (UriContent(uri, html)) = 
    if String.IsNullOrEmpty(html) then Result.Failure [ "empty page" ]
    else 
        let uriContentSize = UriContentSize(uri, html.Length)
        Result.Success uriContentSize

let getUriContentSize uri = getUriContent uri |> Async.map (Result.bind makeContentSize)

let maxContentSize list = 
    let contentSize (UriContentSize(_, len)) = len
    list |> List.maxBy contentSize

let largestPageSizeA urls = 
    urls
    |> List.map (Uri >> getUriContentSize)
    |> List.sequenceAsyncA
    |> Async.map List.sequenceResultA
    |> Async.map (Result.map maxContentSize)
