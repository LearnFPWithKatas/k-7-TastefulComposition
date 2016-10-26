module App.Entry.Tests

open FsUnit.Xunit
open global.Xunit

[<Fact>]
let ``Sample Test``() = 
    App.Entry.main |> should not' (equal null)
