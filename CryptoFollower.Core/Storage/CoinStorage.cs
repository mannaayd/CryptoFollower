using Azure.Data.Tables;
using CryptoFollower.Core.Models;
using CryptoFollower.Core.Storage.DataObjects;
using Microsoft.Extensions.Logging;

namespace CryptoFollower.Core.Storage;

public class CoinStorage : ICoinStorage
{
    private readonly TableServiceClient _service;
    private readonly ILogger<CoinStorage> _logger;
    
    private const string PartitionKeyTemplate = "{0}-coin";

    public CoinStorage(TableServiceClient service, ILogger<CoinStorage> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    public async Task AddCoinData(Coin coin)
    {
        var client = _service.GetTableClient("Coins");
        
        var data = new CoinDo
        {
            PartitionKey = string.Format(PartitionKeyTemplate, coin.Id),
            RowKey = Guid.NewGuid().ToString(),
            Price = coin.Price,
            Capitalization = coin.MarketCapitalization,
            Id = coin.Id
        };
        
        var response = await client.AddEntityAsync(data);
        if (response.IsError)
            _logger.Log(LogLevel.Error, "Error after trying to add coin data to table. Code: {0}, reason: {1}", response.Status, response.ReasonPhrase);
        else 
            _logger.Log(LogLevel.Information, "Successfully added new coin data to table");
    }
}