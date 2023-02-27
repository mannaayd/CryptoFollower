using System.Globalization;
using System.Net;
using Company.CryptoFollower.Models;
using Newtonsoft.Json.Linq;

namespace Company.CryptoFollower.Services;

public class GetCoinInfoService : IGetCoinInfoService
{
    private readonly HttpClient _client;
    
    public GetCoinInfoService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("coingeckoclient");
        _client.BaseAddress = new Uri("https://api.coingecko.com");
    }

    public async Task<Coin> GetCoinInfo(string id, string targetPriceCurrency)
    {
        var coin = new Coin();
        try
        {
            var result =
                await _client.GetAsync("/api/v3/coins/" + id + "?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false&sparkline=false");
            result.EnsureSuccessStatusCode();
            var str =  await result.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(str);
            if (jObject == null)
                throw new NullReferenceException("Cant retrieve coin data from remote API");
            var marketDataObj = (JObject)jObject.GetValue("market_data")!;
            coin.Id = (string)jObject.GetValue("id")!;
            coin.Price = (double)((JObject)marketDataObj.GetValue("current_price")!).GetValue(targetPriceCurrency)!;
            coin.PriceChangePercentage24H = double.Parse((string)marketDataObj.GetValue("price_change_percentage_24h")!, CultureInfo.InvariantCulture);
            coin.MarketCapitalization =  long.Parse((string)((JObject)marketDataObj.GetValue("market_cap")!).GetValue(targetPriceCurrency)!, NumberStyles.Integer);
            coin.Time = DateTimeOffset.Now;
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }
        catch
        {
            Console.WriteLine("HTTP Response was invalid or could not be deserialized.");
        }
        return coin;
    }
}
