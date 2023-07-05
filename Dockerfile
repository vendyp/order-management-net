FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi/src/OrderManagement.WebApi/OrderManagement.WebApi.csproj", "WebApi/src/OrderManagement.WebApi/"]
COPY ["WebApi/src/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "WebApi/src/OrderManagement.Infrastructure/"]
COPY ["WebApi/src/OrderManagement.Core/OrderManagement.Core.csproj", "WebApi/src/OrderManagement.Core/"]
RUN dotnet restore "WebApi/src/OrderManagement.WebApi/OrderManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi/src/OrderManagement.WebApi"
RUN dotnet build "OrderManagement.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagement.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:EnvironmentName=Production

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagement.WebApi.dll"]
