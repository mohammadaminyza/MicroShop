#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi/MicroShop.Catalogs.EndpointApi.csproj", "Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi/"]
RUN dotnet restore "Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi/MicroShop.Catalogs.EndpointApi.csproj"
COPY . .
WORKDIR "/src/Services/Catalogs/03. Endpoint/MicroShop.Catalogs.EndpointApi"
RUN dotnet build "MicroShop.Catalogs.EndpointApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroShop.Catalogs.EndpointApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroShop.Catalogs.EndpointApi.dll"]