namespace ZaraChat.Chat;

using BusinessLogic;

internal static class ChatService
{
    internal static async Task<string> Ask(ApiInput input)
    {
        var response = OpenAICall.GetResponse(input.ChatMessages, input.Token);
        return await response;
    }
}


public record ApiInput(string Token, List<ChatMessage> ChatMessages);