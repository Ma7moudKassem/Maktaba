namespace Maktaba.Services.Order.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;
    private readonly DbSet<Domain.Order> _dbSet;
    private readonly DbSet<User> _users;
    private readonly DbSet<Book> _books;
    private readonly IBookServices _bookServices;
    private readonly IUserServices _userServices;
    public OrderRepository(OrderDbContext context,
        IBookServices bookServices,
        IUserServices userServices)
    {
        _context = context;
        _dbSet = context.Set<Domain.Order>();
        _books = context.Set<Book>();
        _users = context.Set<User>();
        _bookServices = bookServices;
        _userServices = userServices;
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
            if (!await _users.AnyAsync(x => x.UserName == order.UserName, cancellationToken))
            {
                User user = await _userServices.GetUserAsync(order.UserName) ??
                    throw new UserNotProvidedException(order.UserName);

                await _users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }


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
