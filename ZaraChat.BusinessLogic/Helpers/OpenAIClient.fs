namespace ZaraChat.BusinessLogic.Helpers

open System
open System.Net.Http

module OpenAIClient =
    let CreateClient (token: string) =
        let client = new HttpClient()
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}")
        client
