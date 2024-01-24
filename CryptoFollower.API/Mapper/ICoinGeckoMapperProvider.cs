using AutoMapper;

namespace CryptoFollower.API.Mapper;

public interface ICoinGeckoMapperProvider
{
    IMapper Mapper { get; }
}