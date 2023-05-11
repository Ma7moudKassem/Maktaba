namespace Maktaba.Services.Order.Domain;

public class OrderBook : BaseEntity
{
    public Guid OrderId { get; set; }

    public Guid BookId { get; set; }
    public Book? Book { get; set; }
}
