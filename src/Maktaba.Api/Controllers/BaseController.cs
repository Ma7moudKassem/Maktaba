namespace Maktaba.Api;

public class BaseController<TEntity> : ControllerBase
    where TEntity : BaseEntity
{
    private readonly IBaseRepository<TEntity> _repository;
    public BaseController(IBaseRepository<TEntity> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entites = await _repository.GetAsync(cancellationToken);

        return Ok(entites);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetByIdAsync([FromQuery] Guid id,
        CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
            return NotFound();

        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<IActionResult> PostAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(entity, cancellationToken);

            return StatusCode(201);
        }
        catch (Exception exceptions)
        {
            Log.Error(exceptions.GetExceptionErrorSimplified());
            throw;
        }
    }

    [HttpPut]
    public virtual async Task<IActionResult> PutAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TEntity? entityToUpdate = await _repository
                .GetByIdAsync(entity.Id, cancellationToken);

            if (entityToUpdate is null)
                return NotFound();

            await _repository.UpdateAsync(entity, cancellationToken);

            return NoContent();
        }
        catch (Exception exceptions)
        {
            Log.Error(exceptions.GetExceptionErrorSimplified());
            throw;
        }
    }

    [HttpDelete]
    public virtual async Task<IActionResult> DeleteAsync([FromQuery] Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            TEntity? entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return NotFound();

            await _repository.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
        catch (Exception exceptions)
        {
            Log.Error(exceptions.GetExceptionErrorSimplified());
            throw;
        }
    }
}