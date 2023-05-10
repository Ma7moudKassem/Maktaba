namespace Maktaba.Services.Catalog.Application;

public record GetAllBooksQuery() : IRequest<IEnumerable<Book>>;