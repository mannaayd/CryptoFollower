namespace CryptoFollower.API.Services;

public interface ITelegramApiService
{
    Task PostNotification(string message);
}