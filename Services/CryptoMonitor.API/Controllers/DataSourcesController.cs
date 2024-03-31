using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMonitor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourcesController : ControllerBase
    {
        private readonly IRepository<DataSource> _repository;

        public DataSourcesController(IRepository<DataSource> repository)
        {
            this._repository = repository;
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetItemsCount()
        {
            return Ok(await _repository.GetCountAsync());
        }
    }
}
