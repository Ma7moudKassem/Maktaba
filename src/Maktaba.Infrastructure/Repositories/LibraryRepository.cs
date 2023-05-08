namespace Maktaba.Infrastructure;

public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
{
    private readonly DbSet<Library> _libraries;
    public LibraryRepository(MaktabaDbContext context) : base(context) =>
        _libraries = context.Set<Library>();

    public override async Task<IEnumerable<Library>> GetAsync(
        CancellationToken cancellationToken) =>
        await _libraries
            .Include(e => e.LibraryBooks)
            .ThenInclude(e => e.Book)
            .ToListAsync(cancellationToken);

    public override async Task<Library?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken) =>
        await _libraries
            .Include(e => e.LibraryBooks)
            .ThenInclude(e => e.Book)
            .FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);

    public async override Task<IEnumerable<Library>> GetAsync(
        Expression<Func<Library, bool>> predicate,
        CancellationToken cancellationToken) =>
        await _libraries
            .Include(e => e.LibraryBooks)
            .ThenInclude(e => e.Book)
            .Where(predicate)
            .ToListAsync(cancellationToken);
}
