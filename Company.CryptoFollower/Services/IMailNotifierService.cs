namespace Company.CryptoFollower.Services;

public interface IMailNotifierService
{
    Task Notify(string message);
}