global using Maktaba.Services.Basket.gRPC;
global using Maktaba.Services.Basket.Domain;
global using Maktaba.Services.Basket.Api.IntegrationEvents.Events;
global using static Maktaba.Services.Basket.gRPC.Basket;
global using Maktaba.Services.Basket.Infrastructure.Extensions;
global using Maktaba.Services.Basket.Domain.Dtos;
global using Maktaba.Services.Basket.Api.IntegrationEvents.Services;
global using Maktaba.Services.Basket.Api.IntegrationEvents.EventsHandling;

global using EventBus.Events;
global using EventBus.Abstractions;
global using Services.Shared;

global using System.Net;
global using System.Security.Claims;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;

global using Grpc.Core;