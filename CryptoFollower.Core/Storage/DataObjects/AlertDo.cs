using Azure;

namespace CryptoFollower.Core.Storage.DataObjects;

public class AlertDo : Azure.Data.Tables.ITableEntity
{
    public string PartitionKey { get; set; } = string.Empty;
    
    public string RowKey { get; set; } = string.Empty;
    
    public DateTimeOffset? Timestamp { get; set; } 
    
    public ETag ETag { get; set; }
}