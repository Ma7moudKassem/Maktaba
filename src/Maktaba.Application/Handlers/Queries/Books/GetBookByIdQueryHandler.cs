namespace Maktaba.Application;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book?>
{
    private readonly IBookRepository _repository;
    public GetBookByIdQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<Book?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken) =>
        await _repository.GetByIdAsync(request.Id, cancellationToken);
}