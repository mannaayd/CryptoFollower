using Azure;
using Company.CryptoFollower.Models;

namespace Company.CryptoFollower.Storage;

public class CoinTableData : Azure.Data.Tables.ITableEntity
{
    public required string Id { get; set; }
    public required double Price { get; set; }

    public required long Capitalization { get; set; }
    public required string PartitionKey { get; set; }
    public required string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}