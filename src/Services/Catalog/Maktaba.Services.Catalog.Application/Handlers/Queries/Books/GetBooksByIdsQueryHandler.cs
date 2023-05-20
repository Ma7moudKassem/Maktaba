namespace Maktaba.Services.Catalog.Application;

public class GetBooksByIdsQueryHandler : IRequestHandler<GetBooksByIdsQuery,
    IEnumerable<Book>>
{
    private readonly IBookRepository _repository;
    public GetBooksByIdsQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Book>> Handle(GetBooksByIdsQuery request,
        CancellationToken cancellationToken) =>
        await _repository.GetByIdsAsync(request.Ids);
}