global using EventBus;
global using EventBus.Events;
global using EventBus.Extensions;
global using EventBus.Abstractions;
global using EventBusRabbitMQ.Abstractions;

global using System.Text;
global using System.Text.Json;
global using System.Net.Sockets;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;

global using Polly;
global using Polly.Retry;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;