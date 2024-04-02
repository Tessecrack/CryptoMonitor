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

        [HttpGet("exist/id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> ExistId(int id)
        {
            return await _repository.ExistIdAsync(id) ? Ok(true) : NotFound(false);
        }

        [HttpPost("exist")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(DataSource item)
        {
            return await _repository.ExistAsync(item) ? Ok(true) : NotFound(false);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("items[[{skip:int}:{count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count)
        {
            return Ok(await _repository.GetAsync(skip, count));
        }

        [HttpGet("page/{pageIndex:int}/{pageSize:int}")]
        [HttpGet("page/[[{pageIndex:int}/{pageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPage<DataSource>>> GetPage(int pageIndex, int pageSize)
        {
            var result = await _repository.GetPageAsync(pageIndex, pageSize);
            return result.Items.Any() ? Ok(result) : NotFound(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
            => await _repository.GetByIdAsync(id) is { } item ? Ok(item) : NotFound();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(DataSource item)
        {
            var result =await _repository.AddAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(DataSource item)
        {
            if (await  _repository.UpdateAsync(item) is { } result)
            {
                return AcceptedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            return NotFound();
            /*
            return await _repository.UpdateAsync(item) is { } result
                ? AcceptedAtAction(nameof(GetByIdAsync), new { id = result.Id })
                : NotFound();
            */
            /*
            var result = _repository.UpdateAsync(item);
            if (result is null)
            {
                return NotFound();
            }
            return AcceptedAtAction(nameof(GetByIdAsync), new { id = result.Id });
            */
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(DataSource item)
        {
            if (await _repository.DeleteAsync(item) is { } result)
            {
                return Ok(result);
            }
            return NotFound(item);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (await _repository.DeleteByIdAsync(id) is not { } result)
            {
                return NotFound(id);
            }
            return Ok(result);
        }
    }
}
