using CryptoMonitor.DAL.Context;
using CryptoMonitor.DAL.Entities.Base;

namespace CryptoMonitor.DAL.Repositories
{
    public class DbNamedRepository<T> : DbRepository<T> where T : Entity, new()
    {
        public DbNamedRepository(DataDB db) : base(db)
        {

        }
    }
}
