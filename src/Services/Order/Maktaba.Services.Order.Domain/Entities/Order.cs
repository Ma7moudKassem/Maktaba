namespace Maktaba.Services.Order.Domain;

public class Order : BaseEntity
{
    public string UserName { get; set; } = null!;
    public User? User { get; set; }
    public List<OrderBook> OrderBooks { get; set; } = new();
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}