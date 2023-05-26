#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/Maktaba.Services.Identity.Api/Maktaba.Services.Identity.Api.csproj", "src/Services/Identity/Maktaba.Services.Identity.Api/"]
COPY ["src/Services/Identity/Maktaba.Services.Identity.Infrastructure/Maktaba.Services.Identity.Infrastructure.csproj", "src/Services/Identity/Maktaba.Services.Identity.Infrastructure/"]
COPY ["src/Services/Identity/Maktaba.Services.Identity.Domain/Maktaba.Services.Identity.Domain.csproj", "src/Services/Identity/Maktaba.Services.Identity.Domain/"]
COPY ["src/Services/Shared/Services.Shared/Services.Shared.csproj", "src/Services/Shared/Services.Shared/"]
COPY ["src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/EventBus/"]
RUN dotnet restore "src/Services/Identity/Maktaba.Services.Identity.Api/Maktaba.Services.Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Maktaba.Services.Identity.Api"
RUN dotnet build "Maktaba.Services.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maktaba.Services.Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maktaba.Services.Identity.Api.dll"]