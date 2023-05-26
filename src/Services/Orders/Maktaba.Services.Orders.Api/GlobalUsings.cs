global using EventBus.Abstractions;
global using Maktaba.Services.Orders.Domain;
global using Maktaba.Services.Orders.Domain.Events;
global using Maktaba.Services.Orders.Infrastructure;
global using Maktaba.Services.Orders.Application.Extensions;
global using Maktaba.Services.Orders.Application.IntegrationEvents.EventHandling;
global using static Maktaba.Services.Orders.Api.gRPC.Order;

global using Services.Shared;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;