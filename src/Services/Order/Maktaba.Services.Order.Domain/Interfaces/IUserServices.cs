namespace Maktaba.Services.Order.Domain;

public interface IUserServices
{
    Task<User?> GetUserAsync(string userName);
}