namespace Maktaba.Services.Basket.Domain.Dtos
{
    public class UserBasketDto
    {
        public List<BasketItem> Items { get; set; } = new();
    }
}