namespace Maktaba.Services.Orders.Domain.Entities;


public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
}