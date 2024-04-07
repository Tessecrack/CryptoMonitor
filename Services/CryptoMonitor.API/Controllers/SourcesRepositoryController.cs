using AutoMapper;
using CryptoMonitor.API.Controllers.Base;
using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Domain.Base;
using CryptoMonitor.Interfaces.Base.Repositories;

namespace CryptoMonitor.API.Controllers
{
	public class SourcesRepositoryController : MapperEntityController<DataSourceInfo, DataSource>
	{
		public SourcesRepositoryController(IRepository<DataSource> repository, IMapper mapper) 
			: base(repository, mapper)
		{

		}
	}
}
