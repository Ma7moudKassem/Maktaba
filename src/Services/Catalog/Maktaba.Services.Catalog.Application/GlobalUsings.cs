global using Maktaba.Services.Catalog.Domain;
global using Maktaba.Services.Catalog.Infrastructure;

global using EventBus;
global using EventBus.Events;
global using EventBusRabbitMQ;
global using EventBus.Abstractions;
global using EventBusRabbitMQ.Abstractions;

global using System.Reflection;
global using System.Linq.Expressions;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using MediatR;
global using Serilog;
global using RabbitMQ.Client;