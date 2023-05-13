namespace Maktaba.Services.Order.Domain;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetByPredicateAsync(Expression<Func<Order, bool>> predicate,
        CancellationToken cancellationToken = default);
    Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> Exists(Guid id);
}