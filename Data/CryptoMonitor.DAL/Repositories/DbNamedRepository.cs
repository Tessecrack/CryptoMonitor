using CryptoMonitor.DAL.Context;
using CryptoMonitor.DAL.Entities.Base;
using CryptoMonitor.Interfaces.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoMonitor.DAL.Repositories
{
    public class DbNamedRepository<T> : DbRepository<T>, INamedRepository<T> where T : NamedEntity, new()
    {
        public DbNamedRepository(DataDB db) : base(db)
        {

        }

        public async Task<T> DeleteByNameAsync(string name, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(item => item.Name == name);

            if (item is null)
            {
                item = await Set
                    .Select(i => new T { Id = i.Id, Name = i.Name })
                    .FirstOrDefaultAsync(item => item.Name == name, cancel)
                    .ConfigureAwait(false);
            }

            if (item is null) return null;

            return await DeleteAsync(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistNameAsync(string name, CancellationToken cancel = default)
        {
            return await Items.AnyAsync(item => item.Name == name, cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByNameAsync(string name, CancellationToken cancel = default)
        {
            return await Items.FirstOrDefaultAsync(item => item.Name == name, cancel).ConfigureAwait(false);
            //return await Items.SingleOrDefaultAsync(item => item.Name == name, cancel).ConfigureAwait(false);
        }
    }
}
