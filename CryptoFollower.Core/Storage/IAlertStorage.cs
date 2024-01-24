using CryptoFollower.Core.Storage.DataObjects;

namespace CryptoFollower.Core.Storage;

public interface IAlertStorage
{
    Task AddLastAlert(string coinId);

    Task<DateTimeOffset?> GetLastAlert(string coinId);
}