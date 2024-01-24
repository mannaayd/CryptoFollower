using System.Text.Json;
using AutoMapper;
using CryptoFollower.API.Internals;
using CryptoFollower.API.Mapper;
using CryptoFollower.Core.Models;
using NUnit.Framework;

namespace CryptoFollower.Tests.API;

[TestFixture]
public class MapperTests
{
    private IMapper _mapper = null!;

    [SetUp]
    public void SetUp()
    {
        var mapperProvider = new CoinGeckoMapperProvider();
        _mapper = mapperProvider.Mapper;
    }

    [Test]
    [TestCaseSource(nameof(GetMapperTestData))]
    public void MapDtoToModel(CoinOutputDto input, Coin expectedResult)
    {
        // Act
        var res = _mapper.Map<Coin>(input);
        res.Time = default;
        
        // Assert
        Assert.That(JsonSerializer.Serialize(res), Is.EqualTo(JsonSerializer.Serialize(expectedResult)));
        
    }

    #region Test cases

    public static IEnumerable<object[]> GetMapperTestData()
    {
        yield return new object[]
        {
            new CoinOutputDto
            {
                Id = "bitcoin",
                MarketData = new MarketDataDto
                {
                    PriceChangePercentage24H = "23",
                    MarketCapitalization = new MarketCapitalizationDto
                    {
                        Value = "1000000000"
                    },
                    CurrentPrice = new CurrentPriceDto
                    {
                        Value = "39000"
                    }
                }
            },
            new Coin
            {
                Id = "bitcoin",
                Price = 39000,
                MarketCapitalization = 1000000000,
                PriceChangePercentage24H = 23,
                Time = default
            }
        };
    }

    #endregion
}