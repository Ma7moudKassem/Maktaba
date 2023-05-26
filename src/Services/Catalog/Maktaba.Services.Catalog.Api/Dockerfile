FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Catalog/Maktaba.Services.Catalog.Api/Maktaba.Services.Catalog.Api.csproj", "src/Services/Catalog/Maktaba.Services.Catalog.Api/"]
COPY ["src/Services/Catalog/Maktaba.Services.Catalog.Infrastructure/Maktaba.Services.Catalog.Infrastructure.csproj", "src/Services/Catalog/Maktaba.Services.Catalog.Infrastructure/"]
COPY ["src/Services/Catalog/Maktaba.Services.Catalog.Application/Maktaba.Services.Catalog.Application.csproj", "src/Services/Catalog/Maktaba.Services.Catalog.Application/"]
COPY ["src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/EventBus/"]
COPY ["src/Services/Catalog/Maktaba.Services.Catalog.Domain/Maktaba.Services.Catalog.Domain.csproj", "src/Services/Catalog/Maktaba.Services.Catalog.Domain/"]
COPY ["src/Services/Shared/Services.Shared/Services.Shared.csproj", "src/Services/Shared/Services.Shared/"]
RUN dotnet restore "src/Services/Catalog/Maktaba.Services.Catalog.Api/Maktaba.Services.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Maktaba.Services.Catalog.Api"
RUN dotnet build "Maktaba.Services.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maktaba.Services.Catalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maktaba.Services.Catalog.Api.dll"]