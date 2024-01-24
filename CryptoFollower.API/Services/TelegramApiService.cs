using System.Text;
using System.Text.Json;
using CryptoFollower.Core.Settings;
using Microsoft.Extensions.Options;

namespace CryptoFollower.API.Services;

public class TelegramApiService
{
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;
    
    private const string RelativePath = "/bot/{0}/sendMessage";

    public TelegramApiService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
    {
        _client = httpClientFactory.CreateClient("TelegramClient");
        _client.BaseAddress = new Uri("https://api.telegram.org");
        _appSettings = options.Value;
    }

    public async Task PostNotification(string message)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("chat_id", _appSettings.TelegramChatId);
        dictionary.Add("text", message);

        string json = JsonSerializer.Serialize(dictionary);
        var requestData = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(string.Format(RelativePath, _appSettings.TelegramApiKey), requestData);
        response.EnsureSuccessStatusCode();
    }
}