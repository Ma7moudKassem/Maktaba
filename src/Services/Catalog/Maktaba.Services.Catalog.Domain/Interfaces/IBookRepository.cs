namespace Maktaba.Services.Catalog.Domain;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAsync(
        int pageSize = 10, int pageIndex = 0);

    Task<IEnumerable<Book>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<Book>> GetByCategoryAsync(
        Guid categoryId, int pageSize = 10, int pageIndex = 0);

    Task<IEnumerable<Book>> GetBooksWithName(
        string name, int pageIndex = 0, int pageSize = 10);

    Task<Book?> GetByIdAsync(Guid id);

    Task AddBookAsync(Book book);

    Task UpdateBookAsync(Book book);

    Task DeleteBookAsync(Guid id);

    Task<long> BooksTotalCount();

    Task<long> BooksByCategoryTotalCount(Guid categoryId);

    Task<long> BooksByNameTotalCount(string name);
}