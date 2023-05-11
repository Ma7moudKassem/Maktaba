namespace Maktaba.Services.Identity.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;
    public UserRepository(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUserNameAsync(string userName) =>
        await _context.Set<User>().FirstOrDefaultAsync(e => e.UserName == userName);

    public async Task UpdateUserAsync(RegisterModel model)
    {
        User? user = await _context
            .Set<User>()
            .FirstOrDefaultAsync(x => x.UserName == model.Username) ??
            throw new UserNotProvidedException(model.Username);

        user.Email = model.Email;
        user.UserName = model.Username;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.FullAddress = model.FullAddress;

        await Task.Run(() =>
        _context.Set<User>().Update(user));

        await _context.SaveChangesAsync();
    }
}