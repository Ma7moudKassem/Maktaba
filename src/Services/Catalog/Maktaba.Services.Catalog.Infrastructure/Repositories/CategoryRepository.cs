namespace Maktaba.Services.Catalog.Infrastructure;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _dbSet;
    private readonly CatalogDbContext _context;
    public CategoryRepository(CatalogDbContext context)
    {
        _dbSet = context.Set<Category>();
        _context = context;
    }

    public virtual async Task<IEnumerable<Category>> GetAsync(
       CancellationToken cancellationToken = default) =>
       await _dbSet.ToListAsync(cancellationToken);

    public virtual async Task<IEnumerable<Category>> GetAsync(
        Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await _dbSet.Where(predicate).ToListAsync(cancellationToken);

    public virtual async Task<Category?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public virtual async Task AddAsync(Category category,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(Category category,
        CancellationToken cancellationToken = default)
    {
        if (!await Exists(category.Id, cancellationToken))
            throw new EntityNotFoundException(typeof(Category).Name);

        await Task.Run(() => _dbSet.Update(category));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        Category category = await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken) ??
            throw new EntityNotFoundException(typeof(Category).Name);

        await Task.Run(() => _dbSet.Remove(category));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await _dbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);

    public virtual async Task<bool> Exists(Guid id,
        CancellationToken cancellationToken = default) =>
        await _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default) =>
        await _context.Database.BeginTransactionAsync(cancellationToken);
}