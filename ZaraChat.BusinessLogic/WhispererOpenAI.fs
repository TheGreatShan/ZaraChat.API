namespace ZaraChat.BusinessLogic

open System.IO
open System.Net.Http.Headers
open Microsoft.FSharp.Core
open ZaraChat.BusinessLogic.Helpers

module WhispererOpenAI =
    let SpeechToText (content: byte[], fileName: string, token: string) =
        task {
            use client = OpenAIClient.CreateClient(token)
            use form = FormDataConverter.CreateFormData(content, fileName)

            let! transcribe = 
                client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form)
                    |> Async.AwaitTask
                    
            return transcribe.Content.ReadAsStringAsync()
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
            
        }
