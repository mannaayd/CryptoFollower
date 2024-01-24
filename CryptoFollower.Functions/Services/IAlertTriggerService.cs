using CryptoFollower.Core.Models;

namespace CryptoFollower.Functions.Services;

public interface IAlertTriggerService
{
    Task<bool> CheckIfShouldAlert(Coin coin);
}