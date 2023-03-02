using System.Globalization;
using Company.CryptoFollower.Models;
using Company.CryptoFollower.Settings;
using Microsoft.Extensions.Options;

namespace Company.CryptoFollower.Services;

public class AlertTriggerService : IAlertTriggerService
{
    private readonly AppSettings _appSettings;

    public AlertTriggerService(IOptions<AppSettings> options)
    {
        _appSettings = options.Value;
    }

    public bool CheckIfTrigger(Coin coin)
        => _appSettings.IsAlertTriggerAbovePrice
            ? coin.Price > _appSettings.AlertTriggerPrice
            : coin.Price < _appSettings.AlertTriggerPrice;
}