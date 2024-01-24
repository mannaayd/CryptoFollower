using CryptoFollower.Core.Models;

namespace CryptoFollower.Functions.Services;

public interface IUserNotificationService
{ 
    Task Notify(Coin coin);
}