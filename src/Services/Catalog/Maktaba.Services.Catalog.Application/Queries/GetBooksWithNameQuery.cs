namespace Maktaba.Services.Catalog.Application;

public record GetBooksWithNameQuery(string Name, int PageIndex = 0, int PageSize = 10)
    : IRequest<IEnumerable<Book>>;