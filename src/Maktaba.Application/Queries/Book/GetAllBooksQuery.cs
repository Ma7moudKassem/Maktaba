namespace Maktaba.Application;

public record GetAllBooksQuery() : IRequest<IEnumerable<Book>>;