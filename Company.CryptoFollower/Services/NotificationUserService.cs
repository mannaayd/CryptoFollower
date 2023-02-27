using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public class NotificationUserService : INotificationUserService
{
    private readonly ITelegramNotifierService _telegramNotifierService;
    private readonly bool _isNotifiedByTelegram;
    private readonly bool _isNotifiedByMail;
    public NotificationUserService(ITelegramNotifierService telegramNotifierService)
    {
        _telegramNotifierService = telegramNotifierService;
        _isNotifiedByTelegram = bool.Parse(Environment.GetEnvironmentVariable("IsNotifiedByTelegram")!);
        _isNotifiedByMail = bool.Parse(Environment.GetEnvironmentVariable("IsNotifiedByMail")!);
    }

    public void Notify(Coin coin)
    {
        string message = "Alert!\nToken: " + coin.Id + "\nCurrent price: $" + coin.Price + "\n24H change: " +
                         coin.PriceChangePercentage24H + "%\nMarket capitalization: $" + coin.MarketCapitalization;
        if (_isNotifiedByTelegram)
            _telegramNotifierService.Notify(message);
    }
}