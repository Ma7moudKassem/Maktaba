namespace Maktaba.Services.Basket.Api.IntegrationEvents.EventsHandling;

public class BookPriceChangedIntegrationEventHandler : IIntegrationEventHandler<BookPriceChangedIntegrationEvent>
{
    private readonly IBasketRepository _repository;
    private readonly ILogger<BookPriceChangedIntegrationEventHandler> _logger;

    public BookPriceChangedIntegrationEventHandler(IBasketRepository repository,
        ILogger<BookPriceChangedIntegrationEventHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Handle(BookPriceChangedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event {EventId} - ({@event})", @event.Id, @event);

        var users = _repository.GetUsers();

        foreach (var user in users)
        {
            var basket = await _repository.GetBasketAsync(user);

            if (basket is not null)
            {
                basket.Items.Where(x => x.BookId == @event.BookId).ToList()
                    .ForEach(item =>
                    {
                        item.UnitPrice = (decimal)@event.NewPrice;
                        item.OldUnitPrice = (decimal)@event.OldPrice;
                    });

                try
                {
                    _logger.LogInformation("Updating price of books in basket: {BasketId}", basket.UserIdentity);

                    await _repository.AddBasketAsync(basket);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR Updating price of books in basket: {BasketId}", basket.UserIdentity);

                }
            }
        }
    }
}