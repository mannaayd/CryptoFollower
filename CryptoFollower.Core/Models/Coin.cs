﻿namespace CryptoFollower.Core.Models;

public class Coin
{
    public string Id { get; set; } = string.Empty;
    
    public double Price { get; set; } = 0;
    
    public long MarketCapitalization { get; set; } = 0;

    public double PriceChangePercentage24H { get; set; } = 0;
    
    public DateTimeOffset Time { get; set; } = DateTimeOffset.Now;
}