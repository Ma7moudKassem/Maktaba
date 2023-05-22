namespace Maktaba.Services.Basket.Domain;

public interface IBasketRepository
{
    Task<UserBasket?> GetBasketAsync(string userIdentity);
    Task<UserBasket?> AddBasketAsync(UserBasket basket);
    IEnumerable<string> GetUsers();
    Task<bool> DeleteBasketAsync(string userIdentity);
}