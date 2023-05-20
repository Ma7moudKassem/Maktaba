namespace EventBus.Events;

public class IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.UtcNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime creationTime)
    {
        Id = id;
        CreationTime = creationTime;
    }

    [JsonInclude]
    public Guid Id { get; set; }

    [JsonInclude]
    public DateTime CreationTime { get; set; }
}