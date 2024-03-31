using CryptoMonitor.DAL.Context;
using CryptoMonitor.DAL.Entities.Base;
using CryptoMonitor.Interfaces.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoMonitor.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataDB _db;

        protected DbSet<T> Set { get; }

        protected virtual IQueryable<T> Items => Set;

        public bool AutoSaveChanges { get; set; }

        public DbRepository(DataDB db)
        {
            this._db = db;
            Set = _db.Set<T>();
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            //_db.Entry(item).State = EntityState.Added;

            await _db.AddAsync(item, cancel).ConfigureAwait(false);
            //await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

            if (AutoSaveChanges)
            {
                await SaveChangesAsync(cancel).ConfigureAwait(false);
            }

            return item;
        }

        public async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            //_db.Entry(item).State = EntityState.Modified;
            //Set.Update(item);

            _db.Update(item);
            //await _db.SaveChangesAsync(cancel).ConfigureAwait(false);

            if (AutoSaveChanges)
            {
                await SaveChangesAsync(cancel).ConfigureAwait(false);
            }

            return item;
        }

        public async Task<T> DeleteAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            if (!(await ExistIdAsync(item.Id, cancel)))
            {
                return null;
            }

            //_db.Entry(item).State = EntityState.Deleted;
            //Set.Remove(item);

            _db.Remove(item);
            //await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            if (AutoSaveChanges)
            {
                await SaveChangesAsync(cancel).ConfigureAwait(false);
            }

            return item;
        }

        public async Task<T> DeleteByIdAsync(int id, CancellationToken cancel = default)
        {
            var item = Set.Local.FirstOrDefault(item => item.Id == id);

            if (item is null)
            {
                item = await Set
                    .Select(i => new T { Id = i.Id })
                    .FirstOrDefaultAsync(item => item.Id == id, cancel)
                    .ConfigureAwait(false);
            }

            if (item is null) return null;

            return await DeleteAsync(item, cancel).ConfigureAwait(false);
        }

        public async Task<bool> ExistAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            return await Items.AnyAsync(i => i.Id == item.Id, cancel)
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistIdAsync(int id, CancellationToken cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == id, cancel)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancel = default)
        {
            return await Items.ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAsync(int skip, int count, CancellationToken cancel = default)
        {
            /*
            return await Items
                .Skip(skip)
                .Take(count)
                .ToArrayAsync(cancel)
                .ConfigureAwait(false);
            */

            if (count <= 0) return Enumerable.Empty<T>();

            IQueryable<T> query = Items switch
            {
                IOrderedQueryable<T> orderedQuery => orderedQuery,
                { } q => q.OrderBy(i => i.Id),
            };
            if (skip > 0) query = query.Skip(skip);

            return await query.Take(count).ToArrayAsync(cancel).ConfigureAwait(false);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancel = default)
        {
            //return await Items.FirstOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);
            //return await Items.SingleOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);

            switch(Items)
            {
                case DbSet<T> set:
                    return await set.FindAsync(new object[] { id }, cancel).ConfigureAwait(false);
                case { } items:
                    return await items.FirstOrDefaultAsync(item => item.Id == id, cancel).ConfigureAwait(false);
                default:
                    throw new InvalidOperationException("Error: wrong data source");
            }
        }

        public async Task<int> GetCountAsync(CancellationToken cancel = default)
        {
            return await Items.CountAsync()
                .ConfigureAwait(false);
        }

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>;


        public async Task<IPage<T>> GetPageAsync(int pageIndex, int pageSize, CancellationToken cancel = default)
        {
            if (pageSize <= 0) return new Page(Enumerable.Empty<T>(), pageSize, pageIndex, pageSize);
            //if (pageSize <= 0) return new Page(Enumerable.Empty<T>(), await GetCountAsync(cancel).ConfigureAwait(false), pageIndex, pageSize);

            var query = Items;
            var totalCount = await query.CountAsync(cancel).ConfigureAwait(false);
            if (totalCount == 0)
                return new Page(Enumerable.Empty<T>(), 0, pageIndex, pageSize);

            if (query is not IOrderedQueryable<T>)
            {
                query = query.OrderBy(item => item.Id);
            }

            if (pageIndex > 0)
                query = query.Skip(pageIndex * pageSize);
            query = query.Take(pageSize);

            var items = await query.ToArrayAsync(cancel).ConfigureAwait(false);

            return new Page(items, totalCount, pageIndex, pageSize);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancel = default)
        {
            return await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }
    }
}
