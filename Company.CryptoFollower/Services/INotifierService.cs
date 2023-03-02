namespace Company.CryptoFollower.Services;

public interface INotifierService
{
    Task Notify(string message);
}