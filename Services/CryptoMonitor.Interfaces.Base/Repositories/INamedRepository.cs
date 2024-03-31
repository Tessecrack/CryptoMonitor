using CryptoMonitor.Interfaces.Base.Entities;

namespace CryptoMonitor.Interfaces.Base.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T :  INamedEntity
    {
        Task<bool> ExistNameAsync(string name, CancellationToken cancel = default);

        Task<T> GetByNameAsync(string name, CancellationToken cancel = default);

        Task<T> DeleteByNameAsync(string name, CancellationToken cancel = default);
    }
}
