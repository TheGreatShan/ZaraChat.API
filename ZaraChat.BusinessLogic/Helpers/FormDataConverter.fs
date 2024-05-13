namespace ZaraChat.BusinessLogic.Helpers

open System.IO
open System.Net.Http

// TODO TO BE TESTED
module FormDataConverter =
    let CreateFormData (file: Stream, fileName: string) =
        let multipartFormDataContent = new MultipartFormDataContent()
        multipartFormDataContent.Add(new StreamContent(file), "file", fileName)
        multipartFormDataContent.Add(new StringContent("whisper-1"), "model")
        multipartFormDataContent
