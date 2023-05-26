namespace Maktaba.Services.Orders.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetOrderAsync(Guid id,
        CancellationToken cancellationToken = default) =>
        await _context.Orders
        .Include(x => x.Address)
        .Include(x => x.User)
        .ThenInclude(x => x.PaymentMethods)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddOrderAsync(Order order,
        CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrderAsync(Order order,
        CancellationToken cancellationToken = default)
    {
        if (!await Exists(order.Id))
            throw new OrderNotProvidedException(order.Id);

        _context.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrderAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        Order order = await _context
            .Orders
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken)
            ?? throw new OrderNotProvidedException(id);
    }

    public async Task<bool> Exists(Guid id) =>
        await _context.Orders.AnyAsync(x => x.Id == id);
}