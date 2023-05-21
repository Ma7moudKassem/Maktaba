﻿global using Maktaba.Services.Catalog.Api;
global using Maktaba.Services.Catalog.gRPC;
global using Maktaba.Services.Catalog.Domain;
global using Maktaba.Services.Catalog.Application;
global using Maktaba.Services.Catalog.Infrastructure;
global using static Maktaba.Services.Catalog.gRPC.Catalog;

global using System.Net;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.HttpOverrides;
global using Microsoft.AspNetCore.Server.Kestrel.Core;
global using Maktaba.Services.Catalog.Domain.ViewModels;

global using MediatR;
global using Serilog;
global using Grpc.Core;
global using AutoMapper;