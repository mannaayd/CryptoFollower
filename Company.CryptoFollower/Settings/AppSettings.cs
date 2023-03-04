namespace Company.CryptoFollower.Settings;

public record AppSettings
{
    public required string FollowedCryptoCurrency { get; init; }
    public required string TargetPriceCurrencyCode { get; init; }
    public required bool IsAlertTriggerAbovePrice { get; init; }
    public required bool IsNotifiedByMail { get; init; }
    public required bool IsNotifiedByTelegram { get; init; }
    public required double AlertTriggerPrice { get; init; }

    public required int AlertCooldownMinutes { get; init; }
}