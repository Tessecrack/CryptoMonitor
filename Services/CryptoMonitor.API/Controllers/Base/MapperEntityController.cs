using AutoMapper;
using CryptoMonitor.DAL.Entities;
using CryptoMonitor.Interfaces.Base.Entities;
using CryptoMonitor.Interfaces.Base.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CryptoMonitor.API.Controllers.Base
{
	[ApiController, Route("api/[controller]")]
	public abstract class MapperEntityController<T, TBase> : ControllerBase 
        where TBase : IEntity
        where T : IEntity
	{
        private readonly IMapper _mapper;
        private readonly IRepository<TBase> _repository;

        protected MapperEntityController(IRepository<TBase> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

		protected virtual TBase GetBase(T item) => _mapper.Map<TBase>(item);

		protected virtual T GetItem(TBase item) => _mapper.Map<T>(item);

		protected virtual IEnumerable<TBase> GetEnumerableBase(IEnumerable<T> item) 
			=> _mapper.Map<IEnumerable<TBase>>(item);

		protected virtual IEnumerable<T> GetEnumerableItems(IEnumerable<TBase> item)
			=> _mapper.Map<IEnumerable<T>>(item);

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
		public async Task<IActionResult> Exist(T item)
		{
			return await _repository.ExistAsync(GetBase(item)) ? Ok(true) : NotFound(false);
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			return Ok(GetEnumerableItems(await _repository.GetAllAsync()));
		}

		[HttpGet("items[[{skip:int}:{count:int}]]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<DataSource>>> Get(int skip, int count)
		{
			return Ok(GetEnumerableItems(await _repository.GetAsync(skip, count)));
		}

		protected record Page(IEnumerable<T> Items, int TotalCount,
			int PageIndex, int PageSize) : IPage<T>
		{

		}

		protected IPage<T> GetItems(IPage<TBase> page)
			=> new Page(GetEnumerableItems(page.Items), page.TotalCount, page.PageIndex, page.PageSize);

		[HttpGet("page/{pageIndex:int}/{pageSize:int}")]
		[HttpGet("page/[[{pageIndex:int}/{pageSize:int}]]")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IPage<DataSource>>> GetPage(int pageIndex, int pageSize)
		{
			var result = await _repository.GetPageAsync(pageIndex, pageSize);
			return result.Items.Any() ? Ok(GetItems(result)) : NotFound(result);
		}

		[HttpGet("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetById(int id)
			=> await _repository.GetByIdAsync(id) is { } item ? Ok(item) : NotFound();

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Add(T item)
		{
			var result = await _repository.AddAsync(GetBase(item));

			return CreatedAtAction(nameof(GetById), new { id = result.Id }, GetItem(result));
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status202Accepted)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(T item)
		{
			if (await _repository.UpdateAsync(GetBase(item)) is { } result)
			{
				return AcceptedAtAction(nameof(GetById), new { id = result.Id }, GetItem(result));
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
		public async Task<IActionResult> Delete(T item)
		{
			if (await _repository.DeleteAsync(GetBase(item)) is { } result)
			{
				return Ok(GetItem(result));
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
			return Ok(GetItem(result));
		}
	}
}
