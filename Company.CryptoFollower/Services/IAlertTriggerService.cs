using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public interface IAlertTriggerService
{
    Task<bool> CheckIfShouldAlert(Coin coin);
}