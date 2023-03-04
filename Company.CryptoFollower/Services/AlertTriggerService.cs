using System.Globalization;
using Company.CryptoFollower.Models;
using Company.CryptoFollower.Settings;
using Company.CryptoFollower.Storage;
using Microsoft.Extensions.Options;

namespace Company.CryptoFollower.Services;

public class AlertTriggerService : IAlertTriggerService
{
    private readonly AppSettings _appSettings;
    private readonly IAzureTableRepository _repository;

    public AlertTriggerService(IOptions<AppSettings> options, IAzureTableRepository repository)
    {
        _repository = repository;
        _appSettings = options.Value;
    }

    private bool CheckIfTrigger(Coin coin)
        => _appSettings.IsAlertTriggerAbovePrice
            ? coin.Price > _appSettings.AlertTriggerPrice
            : coin.Price < _appSettings.AlertTriggerPrice;

    public async Task<bool> CheckIfShouldAlert(Coin coin)
    {
        if (!CheckIfTrigger(coin))
            return false;
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