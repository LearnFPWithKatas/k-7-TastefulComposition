module App.Async.Tests

open App
open FsCheck
open global.Xunit

let unwrap = Async.RunSynchronously

[<Fact>]
let ``Functor Law - identity``() = 
    let law r = 
        let r = r |> Async.retn
        unwrap (Async.map Operators.id r) = unwrap (Operators.id r)
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Functor Law - composition``() = 
    let law f g v = 
        let v = v |> Async.retn
        unwrap (Async.map (f >> g) v) = unwrap (((Async.map f) >> (Async.map g)) v)
    Check.QuickThrowOnFailure law

let (<*>) = Async.apply

[<Fact>]
let ``Applicative Law - identity``() = 
    let law v = 
        let v = Async.retn v
        unwrap ((Async.retn Operators.id) <*> v) = unwrap v
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Applicative Law - homomorphism``() = 
    let law f x = unwrap ((Async.retn f) <*> (Async.retn x)) = unwrap (Async.retn (f x))
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Applicative Law - interchange``() = 
    let law u y = 
        let u = Async.retn u
        unwrap (u <*> (Async.retn y)) = unwrap ((Async.retn ((|>) y) <*> u))
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Applicative Law - composition``() = 
    let law u v w = 
        let u = Async.retn u
        let v = Async.retn v
        let w = Async.retn w
        unwrap ((Async.retn (<<) <*> u <*> v <*> w)) = unwrap ((u <*> (v <*> w)))
    Check.QuickThrowOnFailure law

let (>>=) p f = Async.bind f p

[<Fact>]
let ``Monad Law - Left identity - Wrap and unwrap round trip``() = 
    let law f a = 
        let f = f >> Async.retn
        unwrap (Async.retn a >>= f) = unwrap (f a)
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Monad Law - Right identity - Unwrap and wrap round trip``() = 
    let law a = 
        let a = Async.retn a
        unwrap (a >>= Async.retn) = unwrap a
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Monad Law - Associative - Unwrap should be associative``() = 
    let law m f g = 
        let m = Async.retn m
        let f = f >> Async.retn
        let g = g >> Async.retn
        unwrap ((m >>= f) >>= g) = unwrap (m >>= ((fun x -> f x >>= g)))
    Check.QuickThrowOnFailure law
