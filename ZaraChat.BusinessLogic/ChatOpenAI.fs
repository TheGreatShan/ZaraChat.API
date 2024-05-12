namespace ZaraChat.BusinessLogic

open System.Net.Http
open System.Text
open System.Text.Json
open System.Text.Json.Serialization

type ChatMessage =
    { [<JsonPropertyName("role")>]
      Role: string
      [<JsonPropertyName("message")>]
      Message: string }

type OpenAIChatMessage =
    { [<JsonPropertyName("role")>]
      Role: string
      [<JsonPropertyName("content")>]
      Content: string }

type RequestChat =
    { [<JsonPropertyName("model")>]
      Model: string
      [<JsonPropertyName("messages")>]
      ChatMessages: List<OpenAIChatMessage> }

module OpenAICall =
    let GetResponse (chatMessages: seq<ChatMessage>, token: string) =
        task {
            use client = new HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}")

            let openAiChatMessages = 
                chatMessages
                |> Seq.map (fun x -> { Role = x.Role; Content = x.Message })
                |> Seq.toList

            // TODO shift to own function
            let requestData =
                { Model = "gpt-4-turbo"
                  ChatMessages = openAiChatMessages }

            let! postAsync =
                client.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json")
                )
                |> Async.AwaitTask

            let! content = postAsync.Content.ReadAsStringAsync() |> Async.AwaitTask

            return content
        }
