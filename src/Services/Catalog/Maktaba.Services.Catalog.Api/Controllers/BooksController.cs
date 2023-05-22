namespace Maktaba.Services.Catalog.Api;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IBookRepository _repository;
    private readonly ILogger<BooksController> _logger;
    public BooksController(
        IMediator mediator,
        IMapper mapper,
        IBookRepository repository,
        ILogger<BooksController> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    //GET api/v1/books?pageSize=10&pageIndex=0
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<BookDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBooks([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        _logger.LogInformation("Send query: {queryName} to get all books", nameof(GetAllBooksQuery));

        IEnumerable<Book> books = await
            _mediator.Send(new GetAllBooksQuery(PageSize: pageSize, PageIndex: pageIndex));

        IEnumerable<BookDto> dtos =
            _mapper.Map<IEnumerable<BookDto>>(books);

        long totalCount = await _repository.BooksTotalCount();

        return Ok(new PaginatedItemsViewModel<BookDto>(
            pageIndex: pageIndex,
            pageSize: pageSize,
            data: dtos,
            count: totalCount));
    }

    //GET api/v1/books/id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<BookDto>> GetBookById([FromRoute] Guid id)
    {
        _logger.LogInformation("Send Query: {QuernName} to get book by id: {BookId}",
            nameof(GetBookByIdQuery), id);

        Book book = await _mediator.Send(new GetBookByIdQuery(id));

        if (book is null)
            return NotFound($"Book with id: {id} is not found");

        BookDto dto = _mapper.Map<BookDto>(book);

        return Ok(dto);
    }

    //GET api/v1/books/withName/name?pageSize=10&pageIndex=0
    [HttpGet("withName/{name}")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<BookDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBooksWithNama(
        [FromRoute] string name, [FromQuery] int pageSize, [FromQuery] int pageIndex)
    {
        _logger.LogInformation("Send Query: {QuernName} to get book by name: {name}",
            nameof(GetBooksWithNameQuery), name);

        long totalCount = await _repository.BooksByNameTotalCount(name);

        IEnumerable<Book> books = await _mediator.Send(new GetBooksWithNameQuery(Name: name, PageIndex: pageIndex, PageSize: pageSize));

        IEnumerable<BookDto> dtos = _mapper.Map<IEnumerable<BookDto>>(books);

        return Ok(new PaginatedItemsViewModel<BookDto>(
            pageIndex: pageIndex,
            pageSize: pageSize,
            count: totalCount,
            data: dtos));
    }


    //GET api/v1/books/withCategory/categoryId?pageSize=10&pageIndex=0
    [HttpGet("withCategory/{categoryId}")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<BookDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBooksWithCategory(
        [FromRoute] Guid categoryId, [FromQuery] int pageSize, [FromQuery] int pageIndex)
    {
        _logger.LogInformation("Send Query: {QuernName} to get book by category id: {categoryId}",
            nameof(GetBooksByCategoryQuery), categoryId);

        long totalCount = await _repository.BooksByCategoryTotalCount(categoryId);

        IEnumerable<Book> books = await _mediator
            .Send(new GetBooksByCategoryQuery(CategoryId: categoryId, PageIndex: pageIndex, PageSize: pageSize));

        IEnumerable<BookDto> dtos = _mapper.Map<IEnumerable<BookDto>>(books);

        return Ok(new PaginatedItemsViewModel<BookDto>(
            pageIndex: pageIndex,
            pageSize: pageSize,
            count: totalCount,
            data: dtos));
    }

    //POST api/v1/books
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> PostBook([FromBody] Book book)
    {
        try
        {
            _logger.LogInformation("Send command: {commandName} to create book with title: {bookName}",
                nameof(AddBookCommand),
                book.Title);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(new AddBookCommand(book));

            return StatusCode(201);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Faild to create book with id: {id} and exception: {exMessage}",
                book.Id,
                exception.GetExceptionErrorSimplified());
            throw;
        }
    }

    //PUT api/v1/books
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> PutBook([FromBody] Book book)
    {
        try
        {
            _logger.LogInformation("Send command: {commandName} to update book with id: {bookId}",
                nameof(UpdateBookCommand),
                book.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mediator.Send(new UpdateBookCommand(book));

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Faild to update book with id: {id} and exception: {exMessage}",
                book.Id,
                exception.GetExceptionErrorSimplified());
            throw;
        }
    }

    //DELETE api/v1/books/id
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> DeleteBook([FromRoute] Guid id)
    {
        try
        {
            _logger.LogInformation("Send command: {commandName} to delete book with id: {bookId}",
                nameof(UpdateBookCommand),
                id);
            await _mediator.Send(new DeleteBookCommand(id));

            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Faild to delete book with id: {id} and exception: {exMessage}",
                id,
                exception.GetExceptionErrorSimplified());
            throw;
        }
    }
}