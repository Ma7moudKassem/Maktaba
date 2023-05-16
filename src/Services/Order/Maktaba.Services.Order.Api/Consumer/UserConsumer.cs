namespace Maktaba.Services.Order.Api;

class UserConsumer : IConsumer<AuthModel>
{
    private readonly OrderDbContext _context;
    public UserConsumer(OrderDbContext context)
    {
        _context = context;
    }
    public async Task Consume(ConsumeContext<AuthModel> context)
    {
        User user = new()
        {
            Email = context.Message.Email,
            FullAddress = context.Message.FullAddress,
            Name = context.Message.Username,
            PhoneNumber = context.Message.PhoneNumber,
            UserName = context.Message.Username,
        };

        await _context.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
    }
}