namespace EventBus.Abstractions;

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;

    void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>;

    void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler;

    bool HasSubscriptionForEvent<T>()
        where T : IntegrationEvent;

    bool HasSubscriptionForEvent(string eventName);

    Type GetEventTypeByName(string eventName);

    void Clear();

    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>()
        where T : IntegrationEvent;

    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

    string GetEventName<T>()
        where T : IntegrationEvent;
}