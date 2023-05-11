namespace Maktaba.Services.Identity.Domain;

public interface IUserRepository
{
    Task<User?> GetUserByUserNameAsync(string userName);
    Task UpdateUserAsync(RegisterModel model);
}