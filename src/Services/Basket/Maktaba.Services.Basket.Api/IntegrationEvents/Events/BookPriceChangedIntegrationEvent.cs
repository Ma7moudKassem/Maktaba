namespace Maktaba.Services.Basket.Api.IntegrationEvents.Events;

public class BookPriceChangedIntegrationEvent : IntegrationEvent
{
    public Guid BookId { get; set; }

    public double OldPrice { get; set; }
    public double NewPrice { get; set; }

    public BookPriceChangedIntegrationEvent(Guid bookId, double oldPrice, double newPrice)
    {
        BookId = bookId;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}