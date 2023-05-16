namespace Maktaba.Services.Order.Domain;

public interface IUserServices
{
    Task<Domain.User?> GetUserAsync(string userName);
}