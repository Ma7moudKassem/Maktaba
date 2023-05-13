namespace Maktaba.Services.Catalog.Api;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public BooksController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        try
        {
            IEnumerable<Book> books = await _mediator.Send(new GetAllBooksQuery());

            IEnumerable<BookDto> dtos = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(dtos);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute] Guid id)
    {
        try
        {
            Book book = await _mediator.Send(new GetBookByIdQuery(id));

            if (book is null)
                return NotFound();

            BookDto dto = _mapper.Map<BookDto>(book);

            return Ok(dto);
        }
        catch (Exception exception)
        {
            Log.Error(exception.GetExceptionErrorSimplified());
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostBook(Book book)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(new AddBookCommand(book));

            return StatusCode(201);
        }
        catch (Exception exceptions)
        {
            Log.Error(exceptions.GetExceptionErrorSimplified());
            throw;
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutBook(Book book)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(new UpdateBookCommand(book));

            return NoContent();
        }
        catch (Exception exceptions)
        {
            Log.Error(exceptions.GetExceptionErrorSimplified());
            throw;
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook([FromQuery] Guid id)
    {
        try
        {
            await _mediator.Send(new DeleteBookCommand(id));

            return NoContent();
        }
        catch (Exception exceptions)
        {
            Log.Error(exceptions.GetExceptionErrorSimplified());
            throw;
        }
    }
}