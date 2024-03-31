using CryptoMonitor.Interfaces.Base.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;

namespace CryptoMonitor.WebAPIClients.Repositories
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly HttpClient _client;

        public WebRepository(HttpClient client)
        {
            _client = client;
        }

        public Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistIdAsync(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(int skip, int count, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync(CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IPage<T>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
