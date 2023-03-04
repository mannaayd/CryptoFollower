using Company.CryptoFollower.Services;
using Company.CryptoFollower.Settings;
using Company.CryptoFollower.Storage;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Company.CryptoFollower;

public class CryptoFollower
{
    private readonly IGetCoinInfoService _getCoinInfoService;
    private readonly INotificationUserService _notificationUserService;
    private readonly IAzureTableRepository _repository;
    private readonly IAlertTriggerService _alertTriggerService;
    private readonly AppSettings _appSettings;
    private readonly ILogger<CryptoFollower> _logger;
    
    public CryptoFollower(IGetCoinInfoService getCoinInfoService, INotificationUserService notificationUserService, IAzureTableRepository repository, IAlertTriggerService alertTriggerService, IOptions<AppSettings> options, ILogger<CryptoFollower> logger)
    {
        _getCoinInfoService = getCoinInfoService;
        _notificationUserService = notificationUserService;
        _repository = repository;
        _alertTriggerService = alertTriggerService;
        _appSettings = options.Value;
        _logger = logger;
    }
    
    // TODO Add notifying user about price changes via email using Azure logic app
    [Function("CryptoFollower")]
    public async Task Run([TimerTrigger("0 */5 * * * *")] MyInfo myTimer, FunctionContext context)
    {
        _logger.Log(LogLevel.Information, "Start retrieving information about {0} coin from CoinGecko API...", _appSettings.FollowedCryptoCurrency);
        var coin = await _getCoinInfoService.GetCoinInfo(_appSettings.FollowedCryptoCurrency, _appSettings.TargetPriceCurrencyCode);
        if(await _alertTriggerService.CheckIfShouldAlert(coin))
            await _notificationUserService.Notify(coin);
        
        var data = new CoinTableData
        {
            PartitionKey = coin.Id + "-coin",
            RowKey = Guid.NewGuid().ToString(),
            Price = coin.Price,
            Capitalization = coin.MarketCapitalization,
            Id = coin.Id
        };
        await _repository.AddCoinData(data);
    }
}

public class MyInfo
{
    public MyScheduleStatus ScheduleStatus { get; set; }

    public bool IsPastDue { get; set; }
}

public class MyScheduleStatus
{
    public DateTime Last { get; set; }

    public DateTime Next { get; set; }

    public DateTime LastUpdated { get; set; }
}