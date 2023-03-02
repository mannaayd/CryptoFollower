namespace Company.CryptoFollower.Settings;

public class AppSettings
{
    public string FollowedCryptoCurrency { get; set; }
    public string TargetPriceCurrencyCode { get; set; }
    public bool IsAlertTriggerAbovePrice { get; set; }
    public bool IsNotifiedByMail { get; set; }
    public bool IsNotifiedByTelegram { get; set; }
    public double AlertTriggerPrice { get; set; }
}