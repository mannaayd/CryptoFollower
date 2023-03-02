namespace Company.CryptoFollower.Storage;

public interface IAzureTableRepository
{
    Task AddCoinData(CoinTableData data);
}