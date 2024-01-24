using CryptoFollower.API.Services;
using CryptoFollower.Core.Models;
using CryptoFollower.Core.Settings;
using CryptoFollower.Core.Storage;
using CryptoFollower.Functions;
using CryptoFollower.Functions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace CryptoFollower.Tests.Functions;

[TestFixture]
public class CryptoFollowerFunctionTests
{
    private CryptoFollowerFunctions _functions = null!;
    private IUserNotificationService _notificationService = null!;
    private IAlertTriggerService _alertTriggerService = null!;
    private IOptions<AppSettings> _options = null!;
    private ILogger<CryptoFollowerFunctions> _logger = null!;
    private ICoinStorage _coinStorage = null!;
    private ICoinGeckoApiService _coinGeckoApiService = null!;
    
    private readonly AppSettings _appSettings = new ()
    {
        CryptoCurrencyToWatch = "bitcoin",
        IsAlertTriggerAbovePrice = true,
        ShouldBeNotifiedByTelegram = false,
        AlertTriggerPrice = 45000,
        AlertCooldownMinutes = 15,
        TelegramApiKey = "key here",
        TelegramChatId = "chat id here"
    };
    
    [SetUp]
    public void SetUp()
    {
        _notificationService = Substitute.For<IUserNotificationService>();
        _alertTriggerService = Substitute.For<IAlertTriggerService>();
        _options = Substitute.For<IOptions<AppSettings>>();
        _options.Value.Returns(_appSettings);
        _logger = Substitute.For<ILogger<CryptoFollowerFunctions>>();
        _coinStorage = Substitute.For<ICoinStorage>();
        _coinGeckoApiService = Substitute.For<ICoinGeckoApiService>();
        
        _functions = new CryptoFollowerFunctions(_notificationService, _alertTriggerService, _options, _logger, _coinStorage, _coinGeckoApiService);
    }

    [Test]
    public async Task CheckCoinInformationTest_NoNotification_CoinAdded()
    {
        // Arrange
        _alertTriggerService.CheckIfShouldAlert(Arg.Any<Coin>()).Returns(false);
        _coinGeckoApiService.GetCoinInformation(Arg.Any<string>()).Returns(new Coin());
        
        // Act
        await _functions.CheckCoinInformation(new TimerInfo(), null);
        
        // Assert
        await _notificationService.DidNotReceive().Notify(Arg.Any<Coin>());
        await _coinStorage.Received(1).AddCoinData(Arg.Any<Coin>());
    }
    
    [Test]
    public async Task CheckCoinInformationTest_Notification_CoinAdded()
    {
        // Arrange
        _alertTriggerService.CheckIfShouldAlert(Arg.Any<Coin>()).Returns(true);
        _coinGeckoApiService.GetCoinInformation(Arg.Any<string>()).Returns(new Coin());
        
        // Act
        await _functions.CheckCoinInformation(new TimerInfo(), null);
        
        // Assert
        await _notificationService.Received(1).Notify(Arg.Any<Coin>());
        await _coinStorage.Received(1).AddCoinData(Arg.Any<Coin>());
    }
    
    [Test]
    public async Task CheckCoinInformationTest_CoinNotAdded()
    {
        // Arrange
        _coinGeckoApiService.GetCoinInformation(Arg.Any<string>()).ReturnsNull();
        
        // Act
        await _functions.CheckCoinInformation(new TimerInfo(), null);
        
        // Assert
        await _notificationService.DidNotReceive().Notify(Arg.Any<Coin>());
        await _coinStorage.DidNotReceive().AddCoinData(Arg.Any<Coin>());
    }
}