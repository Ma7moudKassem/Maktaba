FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Basket/Maktaba.Services.Basket.Api/Maktaba.Services.Basket.Api.csproj", "src/Services/Basket/Maktaba.Services.Basket.Api/"]
COPY ["src/Services/Basket/Maktaba.Services.Basket.Infrastructure/Maktaba.Services.Basket.Infrastructure.csproj", "src/Services/Basket/Maktaba.Services.Basket.Infrastructure/"]
COPY ["src/Services/Basket/Maktaba.Services.Basket.Domain/Maktaba.Services.Basket.Domain.csproj", "src/Services/Basket/Maktaba.Services.Basket.Domain/"]
COPY ["src/Services/Shared/Services.Shared/Services.Shared.csproj", "src/Services/Shared/Services.Shared/"]
COPY ["src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/EventBus/"]
RUN dotnet restore "src/Services/Basket/Maktaba.Services.Basket.Api/Maktaba.Services.Basket.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/Maktaba.Services.Basket.Api"
RUN dotnet build "Maktaba.Services.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maktaba.Services.Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maktaba.Services.Basket.Api.dll"]