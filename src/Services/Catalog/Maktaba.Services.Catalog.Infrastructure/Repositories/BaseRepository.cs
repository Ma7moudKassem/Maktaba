namespace Maktaba.Services.Catalog.Infrastructure;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly CatalogDbContext _context;
    public BaseRepository(CatalogDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        CancellationToken cancellationToken = default) =>
        await _dbSet.ToListAsync(cancellationToken);

    public virtual async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await _dbSet.Where(predicate).ToListAsync(cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

    public virtual async Task AddAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        if (!await Exists(entity.Id, cancellationToken))
            throw new EntityNotFoundException(typeof(TEntity).Name);

        await Task.Run(() => _dbSet.Update(entity));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        if (entity is null)
            throw new EntityNotFoundException(typeof(TEntity).Name);

        await Task.Run(() => _dbSet.Remove(entity));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await _dbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);

    public virtual async Task<bool> Exists(Guid id,
        CancellationToken cancellationToken = default) =>
        await _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default) =>
        await _context.Database.BeginTransactionAsync(cancellationToken);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}