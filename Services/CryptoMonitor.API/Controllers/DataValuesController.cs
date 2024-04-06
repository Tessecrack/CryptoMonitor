using CryptoMonitor.API.Controllers.Base;
using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;

namespace CryptoMonitor.API.Controllers
{
    public class DataValuesController : EntityController<DataValue>
    {
        public DataValuesController(IRepository<DataValue> repository) : base(repository) { }
    }
}
