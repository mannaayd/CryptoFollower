using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public interface INotificationUserService
{ 
    void Notify(Coin coin);
}