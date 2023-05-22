namespace Maktaba.Services.Catalog.Api;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    public CategoriesController(ICategoryRepository repository) =>
        _repository = repository;

    //GET api/v1/Categories
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
    public virtual async Task<IActionResult> GetAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Category> entites = await _repository.GetAsync(cancellationToken);

            return Ok(entites);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }


    //GET api/v1/Categories/{id}
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]

    public virtual async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            Category? entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return NotFound();

            return Ok(entity);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }


    //POST api/v1/Categories
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public virtual async Task<IActionResult> PostAsync(Category entity,
        CancellationToken cancellationToken)
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

    //PUT api/v1/categories
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public virtual async Task<IActionResult> PutAsync(Category entity,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Category? entityToUpdate = await _repository
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

    //DELETE api/v1/categories/{id}
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public virtual async Task<IActionResult> DeleteAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            Category? entity = await _repository.GetByIdAsync(id, cancellationToken);

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