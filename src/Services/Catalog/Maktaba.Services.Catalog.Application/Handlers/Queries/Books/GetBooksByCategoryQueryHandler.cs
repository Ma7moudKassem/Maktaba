namespace Maktaba.Services.Catalog.Application;

public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, IEnumerable<Book>>
{
    private readonly IBookRepository _repository;
    public GetBooksByCategoryQueryHandler(IBookRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<Book>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken) =>
        await _repository.GetByCategoryAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            categoryId: request.CategoryId);
}