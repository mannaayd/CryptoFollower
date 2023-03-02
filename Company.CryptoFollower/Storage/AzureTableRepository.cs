using Azure;
using Azure.Data.Tables;

namespace Company.CryptoFollower.Storage;

public class AzureTableRepository : IAzureTableRepository
{
    private readonly TableServiceClient _service;

    public AzureTableRepository(TableServiceClient service)
    {
        _service = service;
    }


    public async Task AddCoinData(CoinTableData data)
    {
        var client = _service.GetTableClient("Coins");
        var response = await client.AddEntityAsync(data);
        // TODO Add logging on error and success
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
        // TODO Add logging on error and success
    }

    public async Task<AlertTableData?> GetLastAlertData(string partitionKey)
    {
        var client = _service.GetTableClient("Alerts");
        var pages = client.QueryAsync<AlertTableData>(filter: $"PartitionKey eq '{partitionKey}'", maxPerPage: 1);
        await foreach (Page<AlertTableData> page in pages.AsPages())
        {
            return page.Values.First();
        }
        return null;
    }
}