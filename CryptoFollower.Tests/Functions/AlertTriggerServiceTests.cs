using CryptoFollower.Core.Models;
using CryptoFollower.Core.Settings;
using CryptoFollower.Core.Storage;
using CryptoFollower.Core.Storage.DataObjects;
using CryptoFollower.Functions.Services;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;

namespace CryptoFollower.Tests.Functions;

[TestFixture]
public class AlertTriggerServiceTests
{
    private IAlertTriggerService _alertTriggerService = null!;
    private IOptions<AppSettings> _options = null!;
    private IAlertStorage _alertStorage = null!;
    
    [SetUp]
    public void SetUp()
    {
        _alertStorage = Substitute.For<IAlertStorage>();
        _options = Substitute.For<IOptions<AppSettings>>();
    }

    [Test]
    [TestCaseSource(nameof(GetCoinPriceData))]
    public async Task AlertTest(Coin coin, bool isAbovePrice, double triggerPrice, bool expectedResult)
    {
        // Arrange
        var settings = new AppSettings
        {
            IsAlertTriggerAbovePrice = isAbovePrice,
            AlertTriggerPrice = triggerPrice,
            AlertCooldownMinutes = 5,
        };
        _options.Value.Returns(settings);
        _alertStorage.GetLastAlert(Arg.Any<string>()).Returns(DateTimeOffset.Now.AddMinutes(-6));
        _alertTriggerService = new AlertTriggerService(_options, _alertStorage);
        
        // Act
        var res = await _alertTriggerService.CheckIfShouldAlert(coin);
        
        // Assert
        
        Assert.That(res, Is.EqualTo(expectedResult));
    }



    #region Test cases
    
    public static IEnumerable<object[]> GetCoinPriceData()
    {
        yield return new object[]
        {
            new Coin()
            {
                Price = 45001
            },
            true,
            45000,
            true
        };
        
        yield return new object[]
        {
            new Coin()
            {
                Price = 44999
            },
            true,
            45000,
            false
        };
        
        yield return new object[]
        {
            new Coin()
            {
                Price = 45001
            },
            false,
            45000,
            false
        };
        
        yield return new object[]
        {
            new Coin()
            {
                Price = 44999
            },
            false,
            45000,
            true
        };
    }
    
    #endregion
}