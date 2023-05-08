namespace Maktaba.Api;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController<Category>
{
    public CategoriesController(ICategoryRepository repository) : base(repository) { }
}