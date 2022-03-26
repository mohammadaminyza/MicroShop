#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi/MicroShop.Catalogs.EndpointApi.csproj", "src/Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi/"]
COPY ["src/Services/Catalogs/01. Core/MicroShop.Catalogs.Core.ApplicationServices/MicroShop.Catalogs.Core.ApplicationServices.csproj", "src/Services/Catalogs/01. Core/MicroShop.Catalogs.Core.ApplicationServices/"]
COPY ["src/Services/Catalogs/01. Core/MicroShop.Catalogs.Core.Contracts/MicroShop.Catalogs.Core.Contracts.csproj", "src/Services/Catalogs/01. Core/MicroShop.Catalogs.Core.Contracts/"]
COPY ["src/Services/Catalogs/01. Core/MicroShop.Catalogs.Core.Domain/MicroShop.Catalogs.Core.Domain.csproj", "src/Services/Catalogs/01. Core/MicroShop.Catalogs.Core.Domain/"]
COPY ["src/Common/MicroShop.Common/MicroShop.Common.csproj", "src/Common/MicroShop.Common/"]
COPY ["src/Services/Catalogs/02. Infra/Data/MicroShop.Catalogs.Infra.Data.MongoQueries/MicroShop.Catalogs.Infra.Data.MongoQueries.csproj", "src/Services/Catalogs/02. Infra/Data/MicroShop.Catalogs.Infra.Data.MongoQueries/"]
COPY ["src/Services/Catalogs/02. Infra/Data/MicroShop.Catalogs.Data.MongoCommand/MicroShop.Catalogs.Data.MongoCommand.csproj", "src/Services/Catalogs/02. Infra/Data/MicroShop.Catalogs.Data.MongoCommand/"]
RUN dotnet restore "src/Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi/MicroShop.Catalogs.EndpointApi.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi"
RUN dotnet build "MicroShop.Catalogs.EndpointApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroShop.Catalogs.EndpointApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroShop.Catalogs.EndpointApi.dll"]