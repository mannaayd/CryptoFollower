using CryptoFollower.Core.Models;

namespace CryptoFollower.Core.Storage;

public interface ICoinStorage
{
    Task AddCoinData(Coin data);
}