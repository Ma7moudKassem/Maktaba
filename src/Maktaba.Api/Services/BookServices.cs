namespace Maktaba.Api;

public class BookServices : Maktaba.MaktabaBase
{
    private readonly IBookRepository _repository;
    public BookServices(IBookRepository repository)
    {
        _repository = repository;
    }
    public async override Task<StatusMessage> AddBook(BookMessage request, ServerCallContext context)
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
}