namespace Maktaba.Services.Basket.Domain;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(Guid customerId);
    Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
    IEnumerable<string> GetUsers();
    Task<bool> DeleteBasketAsync(string id);
}