module App.Result.Tests

open App
open FsCheck
open global.Xunit

[<Fact>]
let ``Functor Law - identity``() = 
    let law r = (Result.map Operators.id r) = Operators.id r
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Functor Law - composition``() = 
    let law f g v = Result.map (f >> g) v = ((Result.map f) >> (Result.map g)) v
    Check.QuickThrowOnFailure law

let (<*>) = Result.apply

[<Fact>]
let ``Applicative Law - identity``() = 
    let law v = (Result.retn Operators.id) <*> v = v
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Applicative Law - homomorphism``() = 
    let law f x = (Result.retn f) <*> (Result.retn x) = Result.retn (f x)
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Applicative Law - interchange``() = 
    let law u y = u <*> (Result.retn y) = (Result.retn ((|>) y) <*> u)
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Applicative Law - composition``() = 
    let law u v w = (Result.retn (<<) <*> u <*> v <*> w) = (u <*> (v <*> w))
    Check.QuickThrowOnFailure law

let (>>=) p f = Result.bind f p

[<Fact>]
let ``Monad Law - Left identity - Wrap and unwrap round trip``() = 
    let law f a = Result.retn a >>= f = f a
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Monad Law - Right identity - Unwrap and wrap round trip``() = 
    let law a = a >>= Result.retn = a
    Check.QuickThrowOnFailure law

[<Fact>]
let ``Monad Law - Associative - Unwrap should be associative``() = 
    let law m f g = (m >>= f) >>= g = (m >>= ((fun x -> f x >>= g)))
    Check.QuickThrowOnFailure law
