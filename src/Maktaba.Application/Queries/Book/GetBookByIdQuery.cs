namespace Maktaba.Application;

public record GetBookByIdQuery(Guid Id) : IRequest<Book>;