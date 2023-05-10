namespace Maktaba.Services.Catalog.Application;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _repository;
    public GetAllBooksQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, 
        CancellationToken cancellationToken) =>
        await _repository.GetAsync(cancellationToken);
}