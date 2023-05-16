namespace Maktaba.Services.Basket.Api;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string customerId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    IEnumerable<string> GetUsers();
    Task<bool> DeleteBasketAsync(string id);
}