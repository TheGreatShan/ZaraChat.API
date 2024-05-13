namespace ZaraChat.BusinessLogic

open System.IO
open Microsoft.FSharp.Core
open ZaraChat.BusinessLogic.Helpers

module WhispererOpenAI =
    let SpeechToText (content: byte[], fileName: string, token: string) =
        let result =
            task {
                use client = OpenAIClient.CreateClient(token)
                client.DefaultRequestHeaders.Add("Content-Type", "multipart/form-data")
                let form = FormDataConverter.CreateFormData(new MemoryStream(content), fileName)

                let result =
                    client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form)
                    |> Async.AwaitTask
                    |> Async.RunSynchronously

                let content = result.Content.ReadAsStringAsync() |> Async.AwaitTask
                return content
            }

        result
