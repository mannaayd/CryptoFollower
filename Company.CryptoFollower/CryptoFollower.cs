using Company.CryptoFollower.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Configuration;

namespace Company.CryptoFollower;

public class CryptoFollower
{
    private readonly IGetCryptoCurrencyInfoService _getCryptoCurrencyInfoService;

    private readonly string _followedCryptoCurrency;

    private readonly string _targetPriceCurrency;

    public CryptoFollower()
    {
        _followedCryptoCurrency = Environment.GetEnvironmentVariable("FollowedCryptoCurrency")!;
        _targetPriceCurrency = Environment.GetEnvironmentVariable("TargetPriceCurrencyCode")!;
    }

    //  private readonly ILogger<CryptoFollower> _logger;
    public CryptoFollower(IGetCryptoCurrencyInfoService getCryptoCurrencyInfoService
   //     , ILogger<CryptoFollower> logger
        )
    {
        _getCryptoCurrencyInfoService = getCryptoCurrencyInfoService;
    //    _logger = logger;
    }
     
    // TODO Add storing historical data into Azure Data Storage
    // TODO Add notifying user about price changes via email using Azure logic app
    // TODO Optional add notifying via Telegram bot
    [Function("CryptoFollower")]
    public async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] MyInfo myTimer, FunctionContext context)
    {
      //  _logger.Log(LogLevel.Information, "Start retrieving information about");
        var bitcoinInfo = await _getCryptoCurrencyInfoService.GetBitcoinInfo(_followedCryptoCurrency, _targetPriceCurrency);
        Console.WriteLine();
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