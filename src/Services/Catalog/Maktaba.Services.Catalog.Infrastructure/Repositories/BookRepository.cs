namespace Maktaba.Services.Catalog.Infrastructure;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly DbSet<Book> _books;
    public BookRepository(CatalogDbContext context) : base(context) =>
        _books = context.Set<Book>();

    public override async Task<IEnumerable<Book>> GetAsync(CancellationToken cancellationToken) =>
        await _books
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);

    public override async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _books
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public override async Task<IEnumerable<Book>> GetAsync(Expression<Func<Book, bool>> predicate, CancellationToken cancellationToken) =>
        await _books
            .Include(x => x.Category)
            .Where(predicate)
            .ToListAsync(cancellationToken);
}
