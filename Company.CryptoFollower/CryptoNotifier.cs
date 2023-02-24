using Company.CryptoFollower.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.CryptoFollower;

public static class CryptoNotifier
{
    // private readonly IGetCryptoCurrencyInfoService _getCryptoCurrencyInfoService;
    //
    // public CryptoNotifier(IGetCryptoCurrencyInfoService getCryptoCurrencyInfoService)
    // {
    //     _getCryptoCurrencyInfoService = getCryptoCurrencyInfoService;
    // }

    [Function("CryptoNotifier")]
    public static void Run([TimerTrigger("*/15 * * * * *")] MyInfo myTimer, FunctionContext context)
    {
        Console.WriteLine("1");
        
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