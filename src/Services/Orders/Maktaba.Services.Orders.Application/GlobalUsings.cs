global using EventBus.Events;
global using EventBus.Extensions;
global using EventBus.Abstractions;
global using Maktaba.Services.Orders.Domain;
global using Maktaba.Services.Orders.Domain.Entities;
global using Maktaba.Services.Orders.Application.Queries;
global using Maktaba.Services.Orders.Application.Commands;
global using Maktaba.Services.Orders.Application.Behaviors;
global using Maktaba.Services.Orders.Application.IntegrationEvents.Services;
global using Maktaba.Services.Orders.Application.IntegrationEvents.EventHandling;
global using Maktaba.Services.Orders.Application.IntegrationEvents.Events;

global using System.Reflection;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;

global using MediatR;