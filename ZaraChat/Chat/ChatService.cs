using ZaraChat.BusinessLogic.Helpers;

namespace ZaraChat.Chat;

using BusinessLogic;

internal static class ChatService
{
    internal static async Task<IEnumerable<ChatMessage>> Ask(ApiInput input)
    {
        var response = OpenAICall.GetResponse(input.ChatMessages, TokenTransformer.RemoveBearer(input.Token));
        return await response;
    }
}

public record ApiInput(string Token, List<ChatMessage> ChatMessages);