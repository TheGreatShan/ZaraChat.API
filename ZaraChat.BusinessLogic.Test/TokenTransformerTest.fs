module TokenTransformerTest

open Xunit
open ZaraChat.BusinessLogic.Helpers


[<Fact>]
let ``Removes Bearer infront of token`` () =
    let token = "Bearer 1234"
    let shouldToken = "1234"
    let result = token
                 |> TokenTransformer.RemoveBearer
    Assert.Equal(shouldToken, result)
