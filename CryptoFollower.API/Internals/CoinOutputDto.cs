using System.Text.Json.Serialization;

namespace CryptoFollower.API.Internals;

public record CoinOutputDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("market_data")]
    public MarketDataDto MarketData { get; init; } = new ();
}