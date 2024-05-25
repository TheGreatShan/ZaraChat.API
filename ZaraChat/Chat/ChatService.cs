using ZaraChat.BusinessLogic.Helpers;

namespace ZaraChat.Chat;

using BusinessLogic;

internal static class ChatService
{
    internal static async Task<IEnumerable<ChatMessage>> Ask(ApiInput input)
    {
        var response =  OpenAICall.GetResponse(input.ChatMessages, TokenTransformer.RemoveBearer(input.Token)).Result;
        return response;
    }

    internal static Transcription SpeechToText(SpeechToTextInput input)
    {
        var response =
            WhispererOpenAI.SpeechToText(input.Content, input.FileName, TokenTransformer.RemoveBearer(input.Token));
        return response.Result;
    }
}

public record ApiInput(string Token, List<ChatMessage> ChatMessages);

public record SpeechToTextInput(string Token, byte[] Content, string FileName);