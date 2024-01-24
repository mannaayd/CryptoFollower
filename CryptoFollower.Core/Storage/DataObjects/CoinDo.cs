using Azure;

namespace CryptoFollower.Core.Storage.DataObjects;

public class CoinDo : Azure.Data.Tables.ITableEntity
{
    public required string Id { get; set; } = string.Empty;
    
    public required double Price { get; set; } 

    public required long Capitalization { get; set; }
    
    public required string PartitionKey { get; set; } = string.Empty;
    
    public required string RowKey { get; set; } = string.Empty;
    
    public DateTimeOffset? Timestamp { get; set; }
    
    public ETag ETag { get; set; }
}