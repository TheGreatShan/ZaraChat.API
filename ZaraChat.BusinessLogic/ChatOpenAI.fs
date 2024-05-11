namespace ZaraChat.BusinessLogic

open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization

type ChatMessage = { Role: string; Message: string }

type RequestChat =
    { [<JsonPropertyName("model")>]
      Model: string
      [<JsonPropertyName("messages")>]
      ChatMessages: List<ChatMessage> }

module OpenAICall =
    let GetResponse (chatMessages: seq<ChatMessage>, token: string) =
        task {
            use client = new HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}")

            let requestData =
                { Model = "gpt-4-turbo-preview"
                  ChatMessages = List.ofSeq chatMessages }

            let! postAsync =
                client.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(JsonSerializer.Serialize(requestData))
                )
                |> Async.AwaitTask

            let! content = postAsync.Content.ReadAsStringAsync() |> Async.AwaitTask

            return content
        }
