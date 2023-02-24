using System.Net;
using System.Web;
using Company.CryptoFollower.Services.Dto;

namespace Company.CryptoFollower.Services;

public class GetCryptoCurrencyInfoService : IGetCryptoCurrencyInfoService
{
    // TODO add key to Azure Key Vault
    private readonly string API_KEY = "d2bbeda7-77a0-458f-90e9-cffac1401080";
    
    public async Task<CryptoCurrencyInfo> Get()
    {
        try
        {
            Console.WriteLine(makeAPICall());
        }
        catch (WebException e)
        {
            Console.WriteLine(e.Message);
        }

        return new CryptoCurrencyInfo()
        {
        };
    }
    
    private string makeAPICall()
    {
        var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        var queryString = HttpUtility.ParseQueryString(string.Empty);
        queryString["start"] = "1";
        queryString["limit"] = "5000";
        queryString["convert"] = "USD";

        URL.Query = queryString.ToString();

        var client = new WebClient();
        client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.Headers.Add("Accepts", "application/json");
        return client.DownloadString(URL.ToString());

    }
}
