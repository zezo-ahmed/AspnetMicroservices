FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Ordering/API/Ordering.API/Ordering.API.csproj", "Services/Ordering/API/Ordering.API/"]
COPY ["Services/Ordering/Infrastructure/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Ordering/Infrastructure/Ordering.Infrastructure/"]
COPY ["Services/Ordering/Core/Ordering.Application/Ordering.Application.csproj", "Services/Ordering/Core/Ordering.Application/"]
COPY ["Services/Ordering/Core/Ordering.Domain/Ordering.Domain.csproj", "Services/Ordering/Core/Ordering.Domain/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "Services/Ordering/API/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Ordering/API/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]