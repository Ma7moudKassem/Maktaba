﻿namespace EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(IntegrationEvent @event);
}