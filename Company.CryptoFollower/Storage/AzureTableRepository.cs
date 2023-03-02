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
}