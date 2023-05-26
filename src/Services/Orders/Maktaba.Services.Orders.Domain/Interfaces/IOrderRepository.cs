namespace Maktaba.Services.Orders.Domain;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> Exists(Guid id);
}