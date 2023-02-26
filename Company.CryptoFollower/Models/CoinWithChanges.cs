namespace Company.CryptoFollower.Models;

public sealed class CoinWithChanges : Coin
{
    public double PriceChangePercentage24H { get; set; } = 0;
}