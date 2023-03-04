using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Logging;

namespace Company.CryptoFollower.Storage;

public class AzureTableRepository : IAzureTableRepository
{
    private readonly TableServiceClient _service;
    private readonly ILogger<AzureTableRepository> _logger;

    public AzureTableRepository(TableServiceClient service, ILogger<AzureTableRepository> logger)
    {
        _service = service;
        _logger = logger;
    }


    public async Task AddCoinData(CoinTableData data)
    {
        var client = _service.GetTableClient("Coins");
        var response = await client.AddEntityAsync(data);
        if (response.IsError)
            _logger.Log(LogLevel.Error, "Error after trying to add coin data to table. Code: {0}, reason: {1}", response.Status, response.ReasonPhrase);
        else 
            _logger.Log(LogLevel.Information, "Successfully added new coin data to table");
    }

    public async Task AddLastAlertData(string partitionKey)
    {
        var client = _service.GetTableClient("Alerts");
        var alertData = new AlertTableData
        {
            PartitionKey = partitionKey,
            RowKey = (DateTimeOffset.MaxValue.Ticks - DateTimeOffset.Now.Ticks).ToString()
        };
        var response = await client.AddEntityAsync(alertData);
        if (response.IsError)
            _logger.Log(LogLevel.Error, "Error after trying to add data about last alert to table. Code: {0}, reason: {1}", response.Status, response.ReasonPhrase);
        else 
            _logger.Log(LogLevel.Information, "Successfully added new data about last alert to table");
    }

    public async Task<AlertTableData?> GetLastAlertData(string partitionKey)
    {
        _logger.Log(LogLevel.Information, "Retrieving data about last alert...");
        var client = _service.GetTableClient("Alerts");
        var pages = client.QueryAsync<AlertTableData>(filter: $"PartitionKey eq '{partitionKey}'", maxPerPage: 1);
        await foreach (Page<AlertTableData> page in pages.AsPages())
        {
            return page.Values.FirstOrDefault();
        }
        return null;
    }
}