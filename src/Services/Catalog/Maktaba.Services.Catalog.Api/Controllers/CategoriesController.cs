namespace Maktaba.Services.Catalog.Api;

[Route("api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _repository;
    private readonly ILogger<CategoriesController> _logger;
    public CategoriesController(ICategoryRepository repository,
        ILogger<CategoriesController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    //GET api/v1/Categories
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
    public virtual async Task<IActionResult> GetAsync(
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting All Categories...");

        IEnumerable<Category> entites = await _repository.GetAsync(cancellationToken);

        return Ok(entites);
    }

    //GET api/v1/Categories/{id}
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]

    public virtual async Task<IActionResult> GetByIdAsync([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Category With Id: {categoryId}...", id);

        Category? entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
            return NotFound();

        return Ok(entity);
    }


    //POST api/v1/Categories
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public virtual async Task<IActionResult> PostAsync(Category category,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Creating Category...");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(category, cancellationToken);

            return StatusCode(201);
        }
        catch (Exception exceptions)
        {
            _logger.LogError(exceptions, "Failed To Create Category With Id: {id}", category.Id);
            throw;
        }
    }

    //PUT api/v1/categories
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public virtual async Task<IActionResult> PutAsync(Category category,
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Updating Category With Id: {categoryId}...", category.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Category? entityToUpdate = await _repository
                .GetByIdAsync(category.Id, cancellationToken);

            if (entityToUpdate is null)
                return NotFound();

            await _repository.UpdateAsync(category, cancellationToken);

            return NoContent();
        }
        catch (Exception exceptions)
        {
            _logger.LogError(exceptions, "Faild To Update Category With Id: {id}", category.Id);
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
            _logger.LogInformation("Deleting Category With Id: {categoryId}...", id);

            Category? entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return NotFound();

            await _repository.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
        catch (Exception exceptions)
        {
            _logger.LogError(exceptions, "Faild To Delete Category With Id: {id}", id);
            throw;
        }
    }
}