using System.Net;
using Company.CryptoFollower.Models;
using Company.CryptoFollower.Services.Dto;
using Newtonsoft.Json;

namespace Company.CryptoFollower.Services;

public class GetCryptoCurrencyInfoService : IGetCryptoCurrencyInfoService
{
    private readonly HttpClient _client;
    
    public GetCryptoCurrencyInfoService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("coingeckoclient");
        _client.BaseAddress = new Uri("https://api.coingecko.com");
    }

    public async Task<Bitcoin> GetBitcoinInfo(string id, string priceCurrency)
    {
        var bitcoin = new Bitcoin();
        try
        {
            var result =
                await _client.GetAsync("/api/v3/simple/price" + "?ids=" + id + "&" + "vs_currencies=" + priceCurrency);
            result.EnsureSuccessStatusCode();
            var str =  await result.Content.ReadAsStringAsync();
            var bitcoinDto = JsonConvert.DeserializeObject<BitcoinDto>(str);
            if (bitcoinDto == null)
                throw new NullReferenceException("Cant retrieve data from remote API");
            Int32.TryParse(bitcoinDto.bitcoin.usd, out int parseResult);
            bitcoin.price = parseResult;
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }
        catch
        {
            Console.WriteLine("HTTP Response was invalid or could not be deserialised.");
        }
        return bitcoin;
    }
}
