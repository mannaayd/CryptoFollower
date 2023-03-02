using Azure;

namespace Company.CryptoFollower.Storage;

public class AlertTableData : Azure.Data.Tables.ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}