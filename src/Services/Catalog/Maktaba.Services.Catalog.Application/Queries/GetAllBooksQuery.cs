namespace Maktaba.Services.Catalog.Application;

public record GetAllBooksQuery(
        int PageSize = 10,
        int PageIndex = 0) : IRequest<IEnumerable<Book>>;