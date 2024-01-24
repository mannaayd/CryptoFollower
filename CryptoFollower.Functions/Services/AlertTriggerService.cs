using CryptoFollower.Core.Models;
using CryptoFollower.Core.Settings;
using CryptoFollower.Core.Storage;
using Microsoft.Extensions.Options;

namespace CryptoFollower.Functions.Services;

public class AlertTriggerService : IAlertTriggerService
{
    private readonly AppSettings _appSettings;
    private readonly IAlertStorage _alertStorage;

    public AlertTriggerService(IOptions<AppSettings> options, IAlertStorage alertStorage)
    {
        _alertStorage = alertStorage;
        _appSettings = options.Value;
    }

    private bool CheckIfTrigger(Coin coin)
        => _appSettings.IsAlertTriggerAbovePrice
            ? coin.Price > _appSettings.AlertTriggerPrice
            : coin.Price < _appSettings.AlertTriggerPrice;

    public async Task<bool> CheckIfShouldAlert(Coin coin)
    {
        if (!CheckIfTrigger(coin))
        {
            return false;
        }
        
        var lastAlert = await _alertStorage.GetLastAlert(coin.Id);
        
        // Create if not exist
        if (lastAlert == null)
        {
            await _alertStorage.AddLastAlert(coin.Id);
            return false;
        }

        if (DateTimeOffset.Now.Subtract(lastAlert.Value).TotalMinutes < _appSettings.AlertCooldownMinutes)
        {
            return false;
        }
        
        await _alertStorage.AddLastAlert(coin.Id);
        
        return true;
    }
}