namespace Company.CryptoFollower.Storage;

public interface IAzureTableRepository
{
    Task AddCoinData(CoinTableData data);

    Task AddLastAlertData(string partitionKey);

    Task<AlertTableData?> GetLastAlertData(string partitionKey);
}