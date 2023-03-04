using Company.CryptoFollower.Models;
using Company.CryptoFollower.Settings;
using Company.CryptoFollower.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Company.CryptoFollower.Services;

public class NotificationUserService : INotificationUserService
{
    private readonly ITelegramNotifierService _telegramNotifierService;
    private readonly IMailNotifierService _mailNotifierService;
    private readonly AppSettings _appSettings;
    private readonly ILogger<NotificationUserService> _logger;

    public NotificationUserService(ITelegramNotifierService telegramNotifierService, IMailNotifierService mailNotifierService, IOptions<AppSettings> options, ILogger<NotificationUserService> logger)
    {
        _telegramNotifierService = telegramNotifierService;
        _mailNotifierService = mailNotifierService;
        _logger = logger;
        _appSettings = options.Value;
    }

    public async Task Notify(Coin coin)
    {
        string message = "Alert!!!\nToken: " + coin.Id + "\nCurrent price: $" + coin.Price + "\n24H change: " +
                         coin.PriceChangePercentage24H + "%\nMarket capitalization: $" + coin.MarketCapitalization;
        _logger.Log(LogLevel.Information, "Alert user. Message: {0}", message);
        if (_appSettings.IsNotifiedByTelegram)
            await _telegramNotifierService.Notify(message);
        if (_appSettings.IsNotifiedByMail)
            await _mailNotifierService.Notify(message);
    }

    
}