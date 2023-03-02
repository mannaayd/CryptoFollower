using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public interface IAlertTriggerService
{
    bool CheckIfTriggered(Coin coin);
}