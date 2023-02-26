namespace Company.CryptoFollower.Models;

public class Coin
{
    public string Id { get; set; } = string.Empty;
    public double Price { get; set; } = 0;
    public long MarketCapitalization { get; set; } = 0;
    public DateTimeOffset Time { get; set; }
}