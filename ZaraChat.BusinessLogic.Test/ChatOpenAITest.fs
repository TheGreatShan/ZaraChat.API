module ChatOpenAITest

open Xunit
open ZaraChat.BusinessLogic


[<Fact>]
let ``ToOpenAIChatMessage transforms sequence of ChatMessage`` () =
    let messages =
        [ { Role = "user"; Message = "Hello" }
          { Role = "bot"; Message = "Hi" }
          { Role = "user"
            Message = "How are you?" }
          { Role = "bot"
            Message = "I'm fine, thank you" } ]

    let shouldMessage: List<OpenAIChatMessage> =
        [ { Role = "user"; Content = "Hello" }
          { Role = "bot"; Content = "Hi" }
          { Role = "user"
            Content = "How are you?" }
          { Role = "bot"
            Content = "I'm fine, thank you" } ]

    let actual: List<OpenAIChatMessage> =
        messages |> ResponseTransformer.ToOpenAIChatMessage

    Assert.Equal<List<OpenAIChatMessage>>(shouldMessage, actual)
