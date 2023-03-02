namespace Company.CryptoFollower.Services;

public class MailNotifierService : IMailNotifierService
{
    public Task Notify(string message)
    {
        // TODO Implement
        return Task.Delay(1);
    }
}