namespace Maktaba.Services.Catalog.Api;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController<Domain.Category>
{
    public CategoriesController(ICategoryRepository repository) : base(repository) { }
}