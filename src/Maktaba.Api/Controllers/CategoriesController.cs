namespace Maktaba.Api;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController<Maktaba.Domain.Category>
{
    public CategoriesController(ICategoryRepository repository) : base(repository) { }
}