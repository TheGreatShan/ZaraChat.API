namespace ZaraChat.Chat;

internal static class ChatService
{
    internal static List<ChatMessage> Ask(List<ChatMessage> input, string token)
    {
        return new List<ChatMessage>();
    }
}

internal record ChatMessage(string Role, string Message);