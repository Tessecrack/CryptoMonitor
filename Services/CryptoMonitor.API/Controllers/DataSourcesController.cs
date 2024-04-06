using CryptoMonitor.API.Controllers.Base;
using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;

namespace CryptoMonitor.API.Controllers
{
    public class DataSourcesController : EntityController<DataSource>
    {
        public DataSourcesController(IRepository<DataSource> repository) : base(repository) { }
    }
}
