using AutoMapper;
using CryptoFollower.API.Internals;
using CryptoFollower.Core.Models;

namespace CryptoFollower.API.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CoinOutputDto, Coin>()
            .ForMember(x => x.PriceChangePercentage24H, y => y.MapFrom(z => z.MarketData.PriceChangePercentage24H))
            .ForPath(x => x.MarketCapitalization, y => y.MapFrom(z => long.Parse(z.MarketData.MarketCapitalization.Value)))
            .ForPath(x => x.Price, y => y.MapFrom(z => double.Parse(z.MarketData.CurrentPrice.Value)))
            .ForMember(x => x.Time, y => y.MapFrom(z => DateTimeOffset.Now));
    }
}