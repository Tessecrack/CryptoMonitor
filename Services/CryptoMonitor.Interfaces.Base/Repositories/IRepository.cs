using CryptoMonitor.Interfaces.Base.Entities;

namespace CryptoMonitor.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<bool> ExistId(int id);

        Task<bool> Exist(T item);

        Task<int> GetCount();
       
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> Get(int skip, int count);

        Task<IPage<T>> GetPage(int pageIndex,  int pageSize);

        //async Task<T> GetById(int id) => (await GetAllAsync()).FirstOrDefault(item => item.Id == id);

        Task<T> GetById(int id);

        Task<T> Add(T item);

        Task<T> Update(T item);

        Task<T> Delete(T item);

        Task<T> DeleteById(int id);


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
