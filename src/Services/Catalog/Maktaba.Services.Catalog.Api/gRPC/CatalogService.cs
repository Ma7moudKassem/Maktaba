namespace Maktaba.Services.Catalog.Api;

//[Authorize]
public class CatalogService : CatalogBase
{
    private readonly IBookRepository _repository;
    public CatalogService(
        IBookRepository repository)
    {
        _repository = repository;
    }

    public override async Task<BookResponce> GetBookById(BookByIdRequest request, ServerCallContext context)
    {
        var book = await _repository.GetByIdAsync(Guid.Parse(request.Id)) ??
            throw new BookNotProvidedException(request.Id);

        BookResponce responce = new()
        {
            Id = book.Id.ToString(),
            Title = book.Title,
            Price = book.Price,
            CategoryName = book?.Category?.Name,
            Description = book?.Description
        };

        return responce;
    }

    public override async Task<BooksResponce> GetBooksByIds(BooksByIdsRequest request,
        ServerCallContext context)
    {
        IEnumerable<Guid> ids = Enumerable.Empty<Guid>();

        foreach (var id in request.Ids)
            ids = ids.Append(Guid.Parse(id));

        IEnumerable<Book> books = await _repository.GetByIdsAsync(ids);

        return MapToResponce(books);
    }

    public BooksResponce MapToResponce(IEnumerable<Book> books)
    {
        BooksResponce responce = new();

        foreach (Book book in books)
        {
            responce.Data.Add(new BookResponce
            {
                Id = book.Id.ToString(),
                Title = book.Title,
                CategoryName = book.Category?.Name,
                Description = book.Description,
                Price = book.Price
            });
        }

        return responce;
    }
}