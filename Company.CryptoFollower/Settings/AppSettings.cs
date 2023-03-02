namespace Company.CryptoFollower.Settings;

public class AppSettings
{
    public required string FollowedCryptoCurrency { get; set; }
    public required string TargetPriceCurrencyCode { get; set; }
    public required bool IsAlertTriggerAbovePrice { get; set; }
    public required bool IsNotifiedByMail { get; set; }
    public required bool IsNotifiedByTelegram { get; set; }
    public required double AlertTriggerPrice { get; set; }
}