namespace Maktaba.Services.Order.Domain;

public class OrderDto
{
    public string UserName { get; set; } = null!;
    public string UserFullName { get; set; } = null!;
    public string UserFullAddress { get; set; } = null!;
    public User? User { get; set; }
    public List<OrderBook> OrderBooks { get; set; } = new();
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}