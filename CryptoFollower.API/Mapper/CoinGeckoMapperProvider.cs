using AutoMapper;

namespace CryptoFollower.API.Mapper;

public class CoinGeckoMapperProvider : ICoinGeckoMapperProvider
{
    public CoinGeckoMapperProvider()
    {
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        mapperConfig.AssertConfigurationIsValid();
        Mapper = mapperConfig.CreateMapper();
    }

    public IMapper Mapper { get; }
}