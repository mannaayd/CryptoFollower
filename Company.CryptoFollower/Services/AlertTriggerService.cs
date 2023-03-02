using System.Globalization;
using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public class AlertTriggerService : IAlertTriggerService
{
    private readonly bool IsTriggerAbovePrice;
    private readonly double TriggerPrice;

    public AlertTriggerService()
    {
        IsTriggerAbovePrice = bool.Parse(Environment.GetEnvironmentVariable("IsAlertTriggerAbovePrice")!);
        TriggerPrice = double.Parse(Environment.GetEnvironmentVariable("AlertTriggerPrice")!, NumberStyles.Any);
    }

    public bool CheckIfTriggered(Coin coin)
        => IsTriggerAbovePrice
            ? coin.Price > TriggerPrice
            : coin.Price < TriggerPrice;
}