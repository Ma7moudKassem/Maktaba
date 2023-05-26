global using EventBus.Events;
global using EventBus.Abstractions;
global using Maktaba.Services.Orders.Domain.Events;
global using Maktaba.Services.Orders.Domain.Entities;
global using Maktaba.Services.Orders.Application.IntegrationEvents.Services;
global using Maktaba.Services.Orders.Application.IntegrationEvents.EventHandling;

global using System.Reflection;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;

global using MediatR;