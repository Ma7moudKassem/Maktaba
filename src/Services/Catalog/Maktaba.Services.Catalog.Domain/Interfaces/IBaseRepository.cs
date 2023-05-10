namespace Maktaba.Services.Catalog.Domain;

public interface IBaseRepository<TEntity> : IDisposable
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);
    Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}