using CryptoFollower.Core.Models;

namespace CryptoFollower.API.Services;

public interface ICoinGeckoApiService
{
    public Task<Coin?> GetCoinInformation(string id);
}