namespace Maktaba.Services.Catalog.Infrastructure;

public class BookRepository : IBookRepository
{
    private readonly DbSet<Book> _books;
    private readonly CatalogDbContext _context;
    public BookRepository(CatalogDbContext context)
    {
        _context = context;
        _books = context.Set<Book>();
    }

    public async Task<IEnumerable<Book>> GetAsync(
        int pageSize = 10, int pageIndex = 0)
    {
        IEnumerable<Book> books = await _books
            .Include(x => x.Category)
            .OrderBy(x => x.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return books;
    }

    public async Task<IEnumerable<Book>> GetByCategoryAsync(
        Guid categoryId, int pageSize = 10, int pageIndex = 0) =>
        await _books
            .Where(x => x.CategoryId == categoryId)
            .Include(x => x.Category)
            .OrderBy(x => x.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

    public Task<Book?> GetByIdAsync(Guid id) =>
        _books.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Book>> GetByIdsAsync(IEnumerable<Guid> ids) =>
        await _books.Where(x => ids.Contains(x.Id)).Include(x => x.Category).ToListAsync();

    public async Task AddBookAsync(Book book)
    {
        await _books.AddAsync(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _ = await _books.FirstOrDefaultAsync(x => x.Id == book.Id) ??
            throw new BookNotProvidedException(book.Id.ToString());

        _books.Update(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        Book? bookFromDb = await _books.FirstOrDefaultAsync(x => x.Id == id) ??
            throw new BookNotProvidedException(id.ToString());

        _books.Update(bookFromDb);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksWithName(
        string name, int pageIndex = 0, int pageSize = 10) =>
        await _books
        .Where(x => x.Title.StartsWith(name))
        .Include(x => x.Category)
        .Skip(pageSize * pageIndex)
        .Take(pageSize)
        .ToListAsync();

    public async Task<long> BooksTotalCount() =>
       await _books.LongCountAsync();

    public async Task<long> BooksByCategoryTotalCount(Guid categoryId) =>
        await _books
        .Where(x => x.CategoryId == categoryId)
        .LongCountAsync();

    public async Task<long> BooksByNameTotalCount(string name) =>
        await _books
        .Where(x => x.Title.StartsWith(name))
        .LongCountAsync();
}