using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public class NotificationUserService : INotificationUserService
{
    private readonly ITelegramNotifierService _telegramNotifierService;
    private readonly IMailNotifierService _mailNotifierService;
    private readonly bool IsNotifiedByTelegram;
    private readonly bool IsNotifiedByMail;
    public NotificationUserService(ITelegramNotifierService telegramNotifierService, IMailNotifierService mailNotifierService)
    {
        _telegramNotifierService = telegramNotifierService;
        _mailNotifierService = mailNotifierService;
        IsNotifiedByTelegram = bool.Parse(Environment.GetEnvironmentVariable("IsNotifiedByTelegram")!);
        IsNotifiedByMail = bool.Parse(Environment.GetEnvironmentVariable("IsNotifiedByMail")!);
    }

    public void Notify(Coin coin)
    {
        string message = "Alert!\nToken: " + coin.Id + "\nCurrent price: $" + coin.Price + "\n24H change: " +
                         coin.PriceChangePercentage24H + "%\nMarket capitalization: $" + coin.MarketCapitalization;
        if (IsNotifiedByTelegram)
            _telegramNotifierService.Notify(message);
        if (IsNotifiedByMail)
            _mailNotifierService.Notify(message);
    }
}