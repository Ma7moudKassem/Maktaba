namespace EventBus;

public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{
    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
    private readonly List<Type> _eventTypes;
    public event EventHandler<string> OnEventRemoved;

    public InMemoryEventBusSubscriptionsManager()
    {
        _handlers = new();
        _eventTypes = new();
    }

    public bool IsEmpty => _handlers is { Count: 0 };
    public void Clear() =>
        _handlers.Clear();

    public void AddSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        string eventName = GetEventName<T>();

        DoAddSubscription(typeof(TH), eventName, false);

        if (!_eventTypes.Contains(typeof(T)))
            _eventTypes.Add(typeof(T));
    }

    public void AddDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler =>
        DoAddSubscription(typeof(TH), eventName, true);

    public void RemoveSubscription<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var handlerToRemove = FindSubscriptionToRemove<T, TH>();
        string eventNameToRemove = GetEventName<T>();
        DoRemoveHandler(eventNameToRemove, handlerToRemove);
    }

    public void RemoveDynamicSubscription<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler
    {
        var handlerToRemve = FindDynamicSubscriptionToRemove<TH>(eventName);
        DoRemoveHandler(eventName, handlerToRemve);
    }

    public bool HasSubscriptionForEvent<T>()
        where T : IntegrationEvent
    {
        string key = GetEventName<T>();
        return HasSubscriptionForEvent(key);
    }

    public bool HasSubscriptionForEvent(string eventName) =>
        _handlers.ContainsKey(eventName);

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>()
        where T : IntegrationEvent
    {
        string key = GetEventName<T>();
        return GetHandlersForEvent(key);
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) =>
        _handlers[eventName];

    void RaiseOnEventRemove(string eventName)
    {
        var handler = OnEventRemoved;
        handler?.Invoke(this, eventName);
    }

    void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
    {
        if (!HasSubscriptionForEvent(eventName))
            _handlers.Add(eventName, new());

        if (_handlers[eventName].Any(x => x.HandlerType == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already register for '{eventName}'", nameof(handlerType));
        }

        if (isDynamic)
            _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
        else
            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
    }

    void DoRemoveHandler(string eventName, SubscriptionInfo subscriptionToRemove)
    {
        if (subscriptionToRemove is not null)
        {
            _handlers[eventName].Remove(subscriptionToRemove);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);

                Type eventType = _eventTypes.SingleOrDefault(x => x.Name == eventName);

                if (eventType is not null)
                    _eventTypes.Remove(eventType);
            }

            RaiseOnEventRemove(eventName);
        }
    }

    SubscriptionInfo FindSubscriptionToRemove<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        string eventName = GetEventName<T>();
        return DoFindSubscriptionToRemove(eventName, typeof(TH));
    }
    SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName)
        where TH : IDynamicIntegrationEventHandler =>
        DoFindSubscriptionToRemove(eventName, typeof(TH));
    SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
    {
        if (!HasSubscriptionForEvent(eventName))
            return null;

        return _handlers[eventName].SingleOrDefault(x => x.HandlerType == handlerType);
    }

    public Type GetEventTypeByName(string eventName) =>
        _eventTypes.SingleOrDefault(x => x.Name == eventName);
    public string GetEventName<T>()
        where T : IntegrationEvent =>
        typeof(T).Name;
}
