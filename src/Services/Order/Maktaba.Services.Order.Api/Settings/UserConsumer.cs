using Maktaba.Services.Order.Domain;
using Maktaba.Services.Order.Infrastructure;
using MassTransit;

namespace Maktaba.Services.Order.Api;

public class UserConsumer : IConsumer<User>
{
    private readonly OrderDbContext _context;
    public UserConsumer(OrderDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<User> context)
    {
        var data = context.Message;

        await _context.Set<User>().AddAsync(data);
        await _context.SaveChangesAsync();
    }
}