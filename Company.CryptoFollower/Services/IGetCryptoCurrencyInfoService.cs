using Company.CryptoFollower.Services.Dto;

namespace Company.CryptoFollower.Services;

public interface IGetCryptoCurrencyInfoService
{
    public Task<CryptoCurrencyInfo> Get(string id, string priceCurrency);
}