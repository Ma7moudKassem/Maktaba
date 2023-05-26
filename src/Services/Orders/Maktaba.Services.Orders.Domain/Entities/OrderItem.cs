namespace Maktaba.Services.Orders.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid BooktId { get; set; }
    public string BookTitle { get; set; } = null!;
    public decimal BookPrice { get; set; }
    public int Quantity { get; set; }
    public OrderItem(Guid booktId,
        string bookTitle,
        decimal bookPrice,
        int quantity)
    {
        BooktId = booktId;
        BookTitle = bookTitle;
        BookPrice = bookPrice;
        Quantity = quantity;
    }
}