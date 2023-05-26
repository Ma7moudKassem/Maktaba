namespace Maktaba.Services.Orders.Domain.Entities;

public class BasketItem
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string BookName { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public decimal OldUnitPrice { get; set; }
    public int Quantity { get; set; }
    public string? PictureUrl { get; set; }
}