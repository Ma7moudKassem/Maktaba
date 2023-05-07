using Maktaba.Infrastructure;
using Serilog;

namespace Maktaba.Api;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        IEnumerable<Book> books = await _mediator.Send(new GetAllBooksQuery());

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromQuery] Guid id)
    {
        Book book = await _mediator.Send(new GetBookByIdQuery(id));

        if (book is null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(Book book)
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
    public async Task<IActionResult> UpdateBook(Book book)
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
    public async Task<IActionResult> AddBook(Guid id)
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