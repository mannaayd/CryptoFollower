using Company.CryptoFollower.Models;
using Company.CryptoFollower.Services;
using Company.CryptoFollower.Settings;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Options;

namespace Company.CryptoFollower;

public class CurrentCoinInfo
{
    private readonly IGetCoinInfoService _getCoinInfoService;
    private readonly AppSettings _appSettings;
    
    public CurrentCoinInfo(IGetCoinInfoService getCoinInfoService, IOptions<AppSettings> options)
    {
        _getCoinInfoService = getCoinInfoService;
        _appSettings = options.Value;
    }

    [Function("CurrentCoinInfo")]
    public async Task<Coin> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var coin = await _getCoinInfoService.GetCoinInfo(_appSettings.FollowedCryptoCurrency, _appSettings.TargetPriceCurrencyCode);
        return coin;
    }
}