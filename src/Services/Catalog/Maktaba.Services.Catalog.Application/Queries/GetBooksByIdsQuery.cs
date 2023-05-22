namespace Maktaba.Services.Catalog.Application;

public record GetBooksByIdsQuery(IEnumerable<Guid> Ids) :
    IRequest<IEnumerable<Book>>;