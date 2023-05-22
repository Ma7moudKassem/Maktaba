namespace Maktaba.Services.Catalog.Domain;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetAsync(Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Category entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Category entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<Category, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task<bool> Exists(Guid id, CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(
        CancellationToken cancellationToken = default);
}