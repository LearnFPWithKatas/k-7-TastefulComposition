namespace App

type Result<'a> = 
    | Success of 'a
    | Failure of string list

module Result = 
    let map f = 
        function 
        | Success x -> Success(f x)
        | Failure errs -> Failure errs
    
    let retn = Success
    
    let apply fResult xResult = 
        match fResult, xResult with
        | Success f, Success x -> Success(f x)
        | Failure errs, Success _ -> Failure errs
        | Success _, Failure errs -> Failure errs
        | Failure errs1, Failure errs2 -> Failure(errs1 @ errs2)
    
    let bind f = 
        function 
        | Success x -> f x
        | Failure errs -> Failure errs
