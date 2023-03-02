using Company.CryptoFollower.Models;
using Company.CryptoFollower.Settings;
using Company.CryptoFollower.Storage;
using Microsoft.Extensions.Options;

namespace Company.CryptoFollower.Services;

public class NotificationUserService : INotificationUserService
{
    private readonly ITelegramNotifierService _telegramNotifierService;
    private readonly IMailNotifierService _mailNotifierService;
    private readonly IAzureTableRepository _repository;
    private readonly AppSettings _appSettings;
    public NotificationUserService(ITelegramNotifierService telegramNotifierService, IMailNotifierService mailNotifierService, IAzureTableRepository repository, IOptions<AppSettings> options)
    {
        _telegramNotifierService = telegramNotifierService;
        _mailNotifierService = mailNotifierService;
        _repository = repository;
        _appSettings = options.Value;
    }

    public async Task Notify(Coin coin)
    {
        // TODO Add better logging
        if(!await CheckIfShouldAlert(coin))
            return;
        string message = "Alert!\nToken: " + coin.Id + "\nCurrent price: $" + coin.Price + "\n24H change: " +
                         coin.PriceChangePercentage24H + "%\nMarket capitalization: $" + coin.MarketCapitalization;
        if (_appSettings.IsNotifiedByTelegram)
            await _telegramNotifierService.Notify(message);
        if (_appSettings.IsNotifiedByMail)
            await _mailNotifierService.Notify(message);
    }

    private async Task<bool> CheckIfShouldAlert(Coin coin)
    {
        var partitionKey = "alert-" + coin.Id + "-coin";
        var lastAlert = await _repository.GetLastAlertData(partitionKey);
        // Create if not exist
        if (lastAlert == null)
        {
            await _repository.AddLastAlertData(partitionKey);
            return false;
        }
        if(!lastAlert.Timestamp.HasValue || DateTimeOffset.Now.Subtract(lastAlert.Timestamp.Value).TotalMinutes < 5)
            return false;
        await _repository.AddLastAlertData(partitionKey);
        return true;
    }
}