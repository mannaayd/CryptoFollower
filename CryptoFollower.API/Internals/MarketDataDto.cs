using System.Text.Json.Serialization;

namespace CryptoFollower.API.Internals;

public record MarketDataDto
{
    [JsonPropertyName("price_change_percentage_24h")]
    public string PriceChangePercentage24H { get; init; } = string.Empty;
    
    [JsonPropertyName("market_cap")]
    public MarketCapitalizationDto MarketCapitalization { get; init; } = new ();
    
    [JsonPropertyName("current_price")]
    public CurrentPriceDto CurrentPrice { get; init; } = new ();
}

public record CurrentPriceDto
{
    [JsonPropertyName("usd")]
    public string Value { get; init; } = string.Empty;
}

public record MarketCapitalizationDto
{
    [JsonPropertyName("usd")]
    public string Value { get; init; } = string.Empty;
}