namespace Maktaba.Services.Order.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly DbSet<Domain.Order> _dbSet;
    private readonly DbSet<Book> _books;

    private readonly OrderDbContext _context;
    private readonly IBookServices _bookServices;
    public OrderRepository(OrderDbContext context,
        IBookServices bookServices
        )
    {
        _context = context;
        _bookServices = bookServices;
        _books = context.Set<Book>();
        _dbSet = context.Set<Domain.Order>();
    }

    public async Task<bool> Exists(Guid id) =>
        await _dbSet.AnyAsync(x => x.Id == id);

    public async Task<IEnumerable<Domain.Order>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await _dbSet
        .Include(x => x.User)
        .Include(x => x.OrderBooks)
        .ThenInclude(x => x.Book)
        .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Domain.Order>> GetByPredicateAsync(
        Expression<Func<Domain.Order, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        await _dbSet
        .Include(x => x.User)
        .Include(x => x.OrderBooks)
        .ThenInclude(x => x.Book)
        .Where(predicate)
        .ToListAsync(cancellationToken);

    public async Task<Domain.Order?> GetOrderAsync(
        Guid id, CancellationToken cancellationToken = default) =>
        await _dbSet
        .Include(x => x.User)
        .Include(x => x.OrderBooks)
        .ThenInclude(x => x.Book)
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddOrderAsync(Domain.Order order,
        CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var orderBook in order.OrderBooks)
            {
                if (!await _books.AnyAsync(x => x.Id == orderBook.BookId, cancellationToken))
                {
                    Book book = await _bookServices.GetBookByIdAsync(orderBook.BookId);
                    book.Id = orderBook.BookId;
                    await _books.AddAsync(book, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            await _dbSet.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    public async Task UpdateOrderAsync(Domain.Order order,
        CancellationToken cancellationToken = default)
    {
        if (!await Exists(order.Id))
            throw new OrderNotProvidedException();

        await Task.Run(() => _dbSet.Update(order));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteOrderAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (!await Exists(id))
            throw new OrderNotProvidedException();

        await _dbSet.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
