namespace Maktaba.Application;

public record GetBooksByPredicateQuery(Expression<Func<Book, bool>> Expression) : 
    IRequest<IEnumerable<Book>>;
