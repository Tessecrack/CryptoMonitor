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
        public async Task<IActionResult> GetItemsCountAsync()
        {
            return Ok(await _repository.GetCountAsync());
        }

        [HttpGet("exist/id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistIdAsync(int id)
        {
            return await _repository.ExistIdAsync(id) ? Ok(true) : NotFound(false);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("items[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>> GetAsync(int skip, int count)
        {
            return Ok(await _repository.GetAsync(skip, count));
        }
    }
}
