using Company.CryptoFollower.Models;
using Company.CryptoFollower.Services.Dto;

namespace Company.CryptoFollower.Services;

public interface IGetCryptoCurrencyInfoService
{
    public Task<Bitcoin> GetBitcoinInfo(string id, string priceCurrency);
}