using System.Text.Json;

namespace ZaraChat.Helper;

internal static class FileReader
{
    internal static T? ReadFile<T>(string path) =>
        JsonSerializer.Deserialize<T>(File.ReadAllText(path));
}

internal static class AppSettingsReader
{
    internal static AppSettings? ReadAppSettings() =>
        FileReader.ReadFile<AppSettings>("appsettings.json");
}

internal record AppSettings(string ApiKey);