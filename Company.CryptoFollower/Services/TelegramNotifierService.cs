using System.Text;
using Company.CryptoFollower.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Company.CryptoFollower.Services;

public class TelegramNotifierService : ITelegramNotifierService
{
    private readonly HttpClient _client;
    private readonly AppSettings _appSettings;

    public TelegramNotifierService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
    {
        _client = httpClientFactory.CreateClient("telegramclient");
        _client.BaseAddress = new Uri("https://api.telegram.org");
        _appSettings = options.Value;
    }

    public async Task Notify(string message)
    {
        
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("chat_id", _appSettings.TelegramChatId);
        dictionary.Add("text", message);

        string json = JsonConvert.SerializeObject(dictionary);
        var requestData = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(String.Format("https://api.telegram.org/bot" + _appSettings.TelegramApiKey + "/sendMessage"), requestData);
        response.EnsureSuccessStatusCode();
    }
}