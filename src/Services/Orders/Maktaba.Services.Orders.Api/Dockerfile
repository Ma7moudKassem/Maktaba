FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Order/Maktaba.Services.Orders.Api/Maktaba.Services.Orders.Api.csproj", "src/Services/Order/Maktaba.Services.Orders.Api/"]
COPY ["src/Services/Order/Maktaba.Services.Orders.Infrastructure/Maktaba.Services.Orders.Infrastructure.csproj", "src/Services/Order/Maktaba.Services.Orders.Infrastructure/"]
COPY ["src/Services/Order/Maktaba.Services.Orders.Domain/Maktaba.Services.Orders.Domain.csproj", "src/Services/Order/Maktaba.Services.Orders.Domain/"]
COPY ["src/Services/Shared/Services.Shared/Services.Shared.csproj", "src/Services/Shared/Services.Shared/"]
COPY ["src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/EventBus/"]
RUN dotnet restore "src/Services/Order/Maktaba.Services.Orders.Api/Maktaba.Services.Orders.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/Maktaba.Services.Orders.Api"
RUN dotnet build "Maktaba.Services.Orders.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maktaba.Services.Orders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maktaba.Services.Orders.Api.dll"]