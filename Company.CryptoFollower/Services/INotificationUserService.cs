using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public interface INotificationUserService
{ 
    Task Notify(Coin coin);
}