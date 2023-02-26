using Company.CryptoFollower.Services;
using Microsoft.Azure.Functions.Worker;

namespace Company.CryptoFollower;

public class CryptoFollower
{
    private readonly IGetCoinInfoService _getCoinInfoService;
    private readonly INotificationUserService _notificationUserService;

    private readonly string _followedCryptoCurrency;

    private readonly string _targetPriceCurrency;

    //  private readonly ILogger<CryptoFollower> _logger;
    public CryptoFollower(IGetCoinInfoService getCoinInfoService, ITelegramNotifierService telegramNotifierService, INotificationUserService notificationUserService
        //     , ILogger<CryptoFollower> logger
        )
    {
        _getCoinInfoService = getCoinInfoService;
        _notificationUserService = notificationUserService;
        //    _logger = logger;
        _followedCryptoCurrency = Environment.GetEnvironmentVariable("FollowedCryptoCurrency")!;
        _targetPriceCurrency = Environment.GetEnvironmentVariable("TargetPriceCurrencyCode")!;
    }
     
    // TODO Add storing historical data into Azure Data Storage
    // TODO Add notifying user about price changes via email using Azure logic app
    // TODO Optional add notifying via Telegram bot
    // TODO conditional queue trigger for notifications 
    [Function("CryptoFollower")]
    public async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] MyInfo myTimer,
      FunctionContext context)
    {
      //  _logger.Log(LogLevel.Information, "Start retrieving information about");
        var coinInfo = await _getCoinInfoService.GetCoinInfo(_followedCryptoCurrency, _targetPriceCurrency);
        _notificationUserService.Notify(coinInfo);
        // return new CoinTableData
        // {
        //     PartitionKey = "timer",
        //     RowKey = Guid.NewGuid().ToString(),
        //     Price = coinInfo.Price
        // };
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