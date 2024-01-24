using Azure;
using Azure.Data.Tables;
using CryptoFollower.Core.Storage.DataObjects;
using Microsoft.Extensions.Logging;

namespace CryptoFollower.Core.Storage;

public class AlertStorage : IAlertStorage
{
    private readonly TableServiceClient _service;
    private readonly ILogger<AlertStorage> _logger;

    private const string PartitionKeyTemplate = "alert-{0}-coin";
    
    public AlertStorage(TableServiceClient service, ILogger<AlertStorage> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task AddLastAlert(string coinId)
    {
        var client = _service.GetTableClient("Alerts");
        var alertData = new AlertDo
        {
            PartitionKey = string.Format(PartitionKeyTemplate, coinId),
            RowKey = (DateTimeOffset.MaxValue.Ticks - DateTimeOffset.Now.Ticks).ToString()
        };
        var response = await client.AddEntityAsync(alertData);
        if (response.IsError)
            _logger.Log(LogLevel.Error, "Error after trying to add data about last alert to table. Code: {0}, reason: {1}", response.Status, response.ReasonPhrase);
        else 
            _logger.Log(LogLevel.Information, "Successfully added new data about last alert to table");
    }

    public async Task<DateTimeOffset?> GetLastAlert(string coinId)
    {
        _logger.Log(LogLevel.Information, "Retrieving data about last alert...");
        
        var client = _service.GetTableClient("Alerts");
        var partitionKey = string.Format(PartitionKeyTemplate, coinId);
        
        var pages = client.QueryAsync<AlertDo>(filter: $"PartitionKey eq '{partitionKey}'", maxPerPage: 1);
        
        await foreach (Page<AlertDo> page in pages.AsPages())
        {
            return page.Values.FirstOrDefault()?.Timestamp;
        }
        return null;
    }
}