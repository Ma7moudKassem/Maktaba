namespace Maktaba.Services.Catalog.Domain;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
}