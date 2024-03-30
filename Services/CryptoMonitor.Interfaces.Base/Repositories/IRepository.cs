using CryptoMonitor.Interfaces.Base.Entities;

namespace CryptoMonitor.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<bool> ExistIdAsync(int id, CancellationToken cancel = default);

        Task<bool> ExistAsync(T item, CancellationToken cancel = default);

        Task<int> GetCountAsync(CancellationToken cancel = default);
       
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default);

        Task<IEnumerable<T>> GetAsync(int skip, int count, CancellationToken cancel = default);

        Task<IPage<T>> GetPageAsync(int pageIndex,  int pageSize, CancellationToken cancel = default);

        //async Task<T> GetById(int id, CancellationToken cancel = default) => (await GetAllAsync()).FirstOrDefault(item => item.Id == id);

        Task<T> GetByIdAsync(int id, CancellationToken cancel = default);

        Task<T> AddAsync(T item, CancellationToken cancel = default);

        Task<T> UpdateAsync(T item, CancellationToken cancel = default);

        Task<T> DeleteAsync(T item, CancellationToken cancel = default);

        Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default);
    }

    public interface IPage<T>
    {
        IEnumerable<T> Items { get; }

        int TotalCount { get; }

        int PageIndex { get; }

        int PageSize { get; }

        int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / (double)PageSize);
    }
}
