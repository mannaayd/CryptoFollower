namespace CryptoFollower.Core.Settings;

public record AppSettings
{
    public string CryptoCurrencyToWatch { get; init; } = string.Empty;
    
    public bool IsAlertTriggerAbovePrice { get; init; } 
    
    public bool ShouldBeNotifiedByTelegram { get; init; }
    
    public double AlertTriggerPrice { get; init; }
    
    public int AlertCooldownMinutes { get; init; }
    
    // Store in KeyVault
    public string TelegramApiKey { get; init; } = string.Empty;
    
    public string TelegramChatId { get; init; } = string.Empty;

    public string CheckCoinInformationSchedule { get; init; } = string.Empty;
}