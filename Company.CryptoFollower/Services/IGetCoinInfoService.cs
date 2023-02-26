using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Services;

public interface IGetCoinInfoService
{
    public Task<Coin> GetCoinInfo(string id, string targetPriceCurrency);
}