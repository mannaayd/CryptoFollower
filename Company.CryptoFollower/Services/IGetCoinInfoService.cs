using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public interface IGetCoinInfoService
{
    public Task<CoinWithChanges> GetCoinInfo(string id, string targetPriceCurrency);
}