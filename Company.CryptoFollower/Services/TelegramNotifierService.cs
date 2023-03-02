using System.Text;
using Newtonsoft.Json;

namespace Company.CryptoFollower.Services;

public class TelegramNotifierService : ITelegramNotifierService
{
    private readonly HttpClient _client;
    private readonly string API_KEY;

    public TelegramNotifierService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("telegramclient");
        _client.BaseAddress = new Uri("https://api.telegram.org");
        // TODO Add retrieving keys from Azure Key vault
        API_KEY = Environment.GetEnvironmentVariable("TelegramApiKey")!;
    }

    public async Task Notify(string message)
    {
        
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        // TODO Add notification for different subscribers(chat ids)
        dictionary.Add("chat_id", "487222912");
        dictionary.Add("text", message);

        string json = JsonConvert.SerializeObject(dictionary);
        var requestData = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(String.Format("https://api.telegram.org/bot" + API_KEY + "/sendMessage"), requestData);
        response.EnsureSuccessStatusCode();
    }
}