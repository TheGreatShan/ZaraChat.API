namespace ZaraChat.BusinessLogic

open System.Net.Http
open System.Text
open System.Text.Json.Serialization
open Newtonsoft.Json.Linq
open ZaraChat.BusinessLogic.Helpers


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

module ResponseTransformer =
    let ToOpenAIChatMessage (response: seq<ChatMessage>) =
        response
        |> Seq.map (fun x -> { Role = x.Role; Content = x.Message })
        |> Seq.toList

    let ToChatMessage (response: string, chatMessages: seq<ChatMessage>) =
        let openAiResponse = JObject.Parse(response)
        let messageResp = openAiResponse["choices"].First["message"].ToString()

        let deserializedMessage =
            System.Text.Json.JsonSerializer.Deserialize<OpenAIChatMessage>(messageResp)


        List.ofSeq chatMessages
        @ [ { Role = deserializedMessage.Role
              Message = deserializedMessage.Content } ]

module OpenAICall =
    let GetResponse (chatMessages: seq<ChatMessage>, token: string) =
        task {

            use client = OpenAIClient.CreateClient(token)

            let openAiChatMessages = chatMessages |> ResponseTransformer.ToOpenAIChatMessage

            let requestData =
                { Model = "gpt-4o"
                  ChatMessages = openAiChatMessages }

            let! postAsync =
                client.PostAsync(
                    "https://api.openai.com/v1/chat/completions",
                    new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(requestData),
                        Encoding.UTF8,
                        "application/json"
                    )
                )
                |> Async.AwaitTask

            let! content = postAsync.Content.ReadAsStringAsync() |> Async.AwaitTask

            let chatMessages = ResponseTransformer.ToChatMessage(content, chatMessages)
            return chatMessages
        }
