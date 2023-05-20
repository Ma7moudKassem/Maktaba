namespace Maktaba.Services.Catalog.Application.Handlers;

public class GetBooksWithNameQueryHandler : IRequestHandler<GetBooksWithNameQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _repository;
    public GetBooksWithNameQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Book>> Handle(GetBooksWithNameQuery request,
        CancellationToken cancellationToken) =>
        await _repository.GetBooksWithName(
            pageIndex: request.PageIndex, pageSize: request.PageSize, name: request.Name);

}