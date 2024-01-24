using CryptoFollower.API.Services;
using CryptoFollower.Core.Models;
using CryptoFollower.Core.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CryptoFollower.Functions.Services;

public class UserNotificationService : IUserNotificationService
{
    private readonly AppSettings _appSettings;
    private readonly ITelegramApiService _telegramApiService;
    private readonly ILogger<UserNotificationService> _logger;
    
    private const string AlertMessageTemplate = "Alert!!!\nToken: {0}\nCurrent price: {1} usd\n24H change: {2}%\nMarket capitalization: {3} usd";
    
    public UserNotificationService(ITelegramApiService telegramApiService, IOptions<AppSettings> options, ILogger<UserNotificationService> logger)
    {
        _telegramApiService = telegramApiService;
        _logger = logger;
        _appSettings = options.Value;
    }

    public async Task Notify(Coin coin)
    {
        string message = string.Format(AlertMessageTemplate, coin.Id, coin.Price, coin.PriceChangePercentage24H, coin.MarketCapitalization);

        _logger.Log(LogLevel.Information, "Alert user. Message: {0}", message);

        if (_appSettings.ShouldBeNotifiedByTelegram)
        {
            await _telegramApiService.PostNotification(message);
        }
    }

    
}