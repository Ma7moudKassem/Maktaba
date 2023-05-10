namespace Maktaba.Services.Catalog.Api;

[Route("api/[controller]")]
[ApiController]
public class LibrariesController : BaseController<Library>
{
    public LibrariesController(ILibraryRepository repository) : base(repository) { }
}