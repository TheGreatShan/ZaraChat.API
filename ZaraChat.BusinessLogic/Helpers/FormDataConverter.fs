namespace ZaraChat.BusinessLogic.Helpers

open System.Net.Http
open System.Net.Http.Headers

module FormDataConverter =
    let CreateFormData (file: byte[], fileName: string) =
        let multipartFormDataContent = new MultipartFormDataContent()
        let fileContent = new ByteArrayContent(file)
        fileContent.Headers.ContentDisposition <- new ContentDispositionHeaderValue("form-data")
        fileContent.Headers.ContentDisposition.Name <- "\"file\""
        fileContent.Headers.ContentDisposition.FileName <- "\"" + fileName + "\""
        fileContent.Headers.ContentType <- MediaTypeHeaderValue.Parse("application/octet-stream")
        
        multipartFormDataContent.Add(fileContent, "file", fileName)
        multipartFormDataContent.Add(new StringContent("whisper-1"), "model")
        multipartFormDataContent.Add(new StringContent("text"), "response_format")
        multipartFormDataContent
