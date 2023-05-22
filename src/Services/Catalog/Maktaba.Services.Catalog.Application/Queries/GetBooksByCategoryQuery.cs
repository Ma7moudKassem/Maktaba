namespace Maktaba.Services.Catalog.Application;

public record GetBooksByCategoryQuery(
        Guid CategoryId,
        int PageSize = 10,
        int PageIndex = 0) : IRequest<IEnumerable<Book>>;