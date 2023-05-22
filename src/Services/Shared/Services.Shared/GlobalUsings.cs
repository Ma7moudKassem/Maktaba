global using EventBus;
global using EventBusRabbitMQ;
global using EventBus.Abstractions;
global using EventBusRabbitMQ.Abstractions;

global using System.Text;

global using Microsoft.Extensions.Hosting;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Authentication.JwtBearer;

global using Serilog;
global using Serilog.Events;
global using RabbitMQ.Client;