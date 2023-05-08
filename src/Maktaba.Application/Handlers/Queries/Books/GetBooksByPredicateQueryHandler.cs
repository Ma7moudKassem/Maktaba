namespace Maktaba.Application;

public class GetBooksByPredicateQueryHandler : IRequestHandler<GetBooksByPredicateQuery, 
    IEnumerable<Book>>
{
    private readonly IBookRepository _repository;
    public GetBooksByPredicateQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Book>> Handle(GetBooksByPredicateQuery request, 
        CancellationToken cancellationToken) =>
        await _repository.GetAsync(request.Expression, cancellationToken);
}