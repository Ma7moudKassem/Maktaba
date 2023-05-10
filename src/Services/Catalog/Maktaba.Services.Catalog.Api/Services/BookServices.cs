namespace Maktaba.Services.Catalog.Api;

//[Authorize]
public class BookServices : MaktabaBase
{
    private readonly IBookRepository _repository;
    public BookServices(
        IBookRepository repository)
    {
        _repository = repository;
    }
    public async override Task<StatusMessage> AddBook(BookMessage request,
        ServerCallContext context)
    {
        Book book = new()
        {
            Title = request.Title,
            Price = request.Price,
            Category = new()
            {
                Name = request.CategoryName
            },
            Description = request.Description,
        };

        await _repository.AddAsync(book, default);

        return new StatusMessage()
        {
            Message = "Success",
            Status = ReadingStatus.Success,
        };
    }

    public override async Task<StatusMessage> UpdateBook(BookToUpdateMessage request,
        ServerCallContext context)
    {
        Book? book = await _repository.GetByIdAsync(new Guid(request.Id), default);

        if (book is null)
            throw new EntityNotFoundException(nameof(Book));

        book.Price = request.Price;
        book.Title = request.Title;
        book.Description = request.Description;
        book.Category.Name = request.Category.Name;
        book.CategoryId = new Guid(request.CategoryId);

        await _repository.UpdateAsync(book, default);
        return new StatusMessage()
        {
            Message = "Data Updated Succeful",
            Status = ReadingStatus.Success,
        };
    }

    public override async Task<BookMessage> GetBook(UUID request, ServerCallContext context)
    {
        Book? book = await _repository.GetByIdAsync(new Guid(request.Id), default) ??
            throw new EntityNotFoundException(nameof(Book));

        return new BookMessage
        {
            Title = book.Title,
            CategoryName = book.Category.Name,
            Description = book.Description,
            Price = book.Price,
        };
    }

    public override async Task<Empty> AddReadingStream(IAsyncStreamReader<BookMessage> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var msg = requestStream.Current;
        }

        return new Empty();
    }
}