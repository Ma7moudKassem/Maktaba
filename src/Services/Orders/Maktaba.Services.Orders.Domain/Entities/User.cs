namespace Maktaba.Services.Orders.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public List<PaymentMethod>? PaymentMethods { get; set; }

    public User()
    {
        PaymentMethods = new();
    }

    public User(string name, Guid id)
    {
        Id = id;
        Name = name;
    }
}