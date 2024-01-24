using System.Globalization;
using System.Net;
using System.Text.Json;
using AutoMapper;
using CryptoFollower.API.Internals;
using CryptoFollower.API.Mapper;
using CryptoFollower.Core.Models;

namespace CryptoFollower.API.Services;

public class CoinGeckoApiService : ICoinGeckoApiService
{
    private readonly HttpClient _client;
    private IMapper _mapper;
    
    private const string RelativeUriPath = "/api/v3/coins/{0}?localization=false&tickers=false&market_data=true&community_data=false&developer_data=false&sparkline=false";
    
    public CoinGeckoApiService(IHttpClientFactory httpClientFactory, ICoinGeckoMapperProvider mapperProvider)
    {
        _mapper = mapperProvider.Mapper;
        _client = httpClientFactory.CreateClient("CoinGecko");
        _client.BaseAddress = new Uri("https://api.coingecko.com");
    }

    public async Task<Coin?> GetCoinInformation(string id)
    {
        try
        {
            var result = await _client.GetAsync(string.Format(RelativeUriPath, id));
            result.EnsureSuccessStatusCode();
            
            var outputCoin = await JsonSerializer.DeserializeAsync<CoinOutputDto>(await result.Content.ReadAsStreamAsync());
            
            return _mapper.Map<Coin>(outputCoin);
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }
        catch
        {
            Console.WriteLine("HTTP Response was invalid or could not be deserialized.");
        }
        return null;
    }
}