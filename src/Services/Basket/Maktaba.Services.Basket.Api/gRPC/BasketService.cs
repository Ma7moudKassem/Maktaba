using Maktaba.Services.Basket.Domain;

namespace Maktaba.Services.Basket;

public class BasketService : BasketBase
{
    private readonly IBasketRepository _repository;
    public BasketService(IBasketRepository repository)
    {
        _repository = repository;
    }

    public override async Task<CustomerBasketResponse> GetBasketById(BasketRequest request, ServerCallContext context)
    {
        CustomerBasket? basket = await _repository.GetBasketAsync(new Guid(request.Id));

        if (basket is not null)
        {
            context.Status = new Status(StatusCode.OK, $"Basket with id: {request.Id} is found");

            return MapToCustomerBasketResponse(basket);
        }
        else
        {
            context.Status = new Status(StatusCode.NotFound, $"Basket with id {request.Id} do not exist");
        }

        return new CustomerBasketResponse();
    }

    CustomerBasketResponse MapToCustomerBasketResponse(CustomerBasket basket)
    {
        CustomerBasketResponse response = new()
        {
            UserId = basket.BuyerId.ToString(),
        };

        basket.Items.ForEach(x =>
        {
            response.Items.Add(new BasketItemResponce
            {
                Id = x.Id.ToString(),
                BookId = x.BookId.ToString(),
                BookTitle = x.BookName,
                Price = (double)x.UnitPrice,
                OldPrice = (double)x.OldUnitPrice,
                Pictureurl = x.PictureUrl,
                Quantity = x.Quantity
            });
        });

        return response;
    }
}