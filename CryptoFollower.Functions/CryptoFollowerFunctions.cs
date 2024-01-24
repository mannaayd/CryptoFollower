using CryptoFollower.API.Services;
using CryptoFollower.Core.Settings;
using CryptoFollower.Core.Storage;
using CryptoFollower.Functions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CryptoFollower.Functions;

public class CryptoFollowerFunctions
{
    private readonly ICoinGeckoApiService _coinGeckoApiService;
    private readonly IUserNotificationService _userNotificationService;
    private readonly ICoinStorage _coinStorage;
    private readonly IAlertTriggerService _alertTriggerService;
    private readonly AppSettings _appSettings;
    private readonly ILogger<CryptoFollowerFunctions> _logger;
    
    public CryptoFollowerFunctions(IUserNotificationService userNotificationService, IAlertTriggerService alertTriggerService, IOptions<AppSettings> options, ILogger<CryptoFollowerFunctions> logger, ICoinStorage coinStorage, ICoinGeckoApiService coinGeckoApiService)
    {
        _userNotificationService = userNotificationService;
        _alertTriggerService = alertTriggerService;
        _appSettings = options.Value;
        _logger = logger;
        _coinStorage = coinStorage;
        _coinGeckoApiService = coinGeckoApiService;
    }
    
    [Function(nameof(CheckCoinInformation))]
    public async Task CheckCoinInformation([TimerTrigger("%CheckCoinInformationSchedule%")] TimerInfo timer, FunctionContext context)
    {
        _logger.Log(LogLevel.Information, "Start retrieving information about {0} coin from CoinGecko API...", _appSettings.CryptoCurrencyToWatch);
        
        var coin = await _coinGeckoApiService.GetCoinInformation(_appSettings.CryptoCurrencyToWatch);

        if (coin != null)
        {
            if (await _alertTriggerService.CheckIfShouldAlert(coin))
            {
                await _userNotificationService.Notify(coin);
            }
            
            await _coinStorage.AddCoinData(coin);
        }
    }
}