module App.List

let traverseAsyncA f list = 
    let (<*>) = Async.apply
    let retn = Async.retn

    let cons head tail = head :: tail
    let initState = retn []

    let folder head tail = 
        retn cons <*> (f head) <*> tail

    List.foldBack folder list initState

let sequenceAsyncA x = traverseAsyncA id x

let traverseResultA f list = 
    let (<*>) = Result.apply
    let retn = Result.Success

    let cons head tail = head :: tail
    let initState = retn []

    let folder head tail = 
        retn cons <*> (f head) <*> tail

    List.foldBack folder list initState

let sequenceResultA x = traverseResultA id x
