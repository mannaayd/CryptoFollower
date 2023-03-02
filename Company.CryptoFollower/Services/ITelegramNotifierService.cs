namespace Company.CryptoFollower.Services;

public interface ITelegramNotifierService
{
    Task Notify(string message);
}