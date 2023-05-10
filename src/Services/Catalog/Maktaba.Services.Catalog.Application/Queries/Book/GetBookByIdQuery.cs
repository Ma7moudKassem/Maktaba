namespace Maktaba.Services.Catalog.Application;

public record GetBookByIdQuery(Guid Id) : IRequest<Book>;