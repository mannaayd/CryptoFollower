using System.Net;
using System.Web;
using Company.CryptoFollower.Services.Dto;

namespace Company.CryptoFollower.Services;

public class GetCryptoCurrencyInfoService : IGetCryptoCurrencyInfoService
{
    private readonly HttpClient _client;
    
    public GetCryptoCurrencyInfoService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("coingeckoclient");
        _client.BaseAddress = new Uri("https://api.coingecko.com");
    }

    public async Task<CryptoCurrencyInfo> Get(string id, string priceCurrency)
    {
        try
        {
            var result =
                await _client.GetAsync("/api/v3/simple/price" + "?ids=" + id + "&" + "vs_currencies=" + priceCurrency);
            result.EnsureSuccessStatusCode();
            var str =  await result.Content.ReadAsStringAsync();
            Console.WriteLine(str);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }
        catch
        {
            Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
        }

        return new CryptoCurrencyInfo();
        
    }
}
