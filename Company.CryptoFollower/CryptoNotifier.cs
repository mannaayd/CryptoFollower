using Company.CryptoFollower.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.CryptoFollower;

public class CryptoNotifier
{
    private readonly IGetCryptoCurrencyInfoService _getCryptoCurrencyInfoService;
    
    public CryptoNotifier(IGetCryptoCurrencyInfoService getCryptoCurrencyInfoService)
    {
        _getCryptoCurrencyInfoService = getCryptoCurrencyInfoService;
    }
    

    [Function("CryptoNotifier")]
    public void Run([TimerTrigger("*/15 * * * * *")] MyInfo myTimer, FunctionContext context)
    {
        _getCryptoCurrencyInfoService.Get("bitcoin", "usd");

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