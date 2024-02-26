#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Dotnet.Sales/Dotnet.Sales.csproj", "src/Dotnet.Sales/"]
COPY ["src/Dotnet.Sales.Application/Dotnet.Sales.Application.csproj", "src/Dotnet.Sales.Application/"]
COPY ["src/Dotnet.Sales.Infrastructure.PostgreSQL/Dotnet.Sales.Infrastructure.PostgreSQL.csproj", "src/Dotnet.Sales.Infrastructure.PostgreSQL/"]
COPY ["src/Dotnet.Sales.WebApi/Dotnet.Sales.WebApi.csproj", "src/Dotnet.Sales.WebApi/"]
RUN dotnet restore "./src/Dotnet.Sales.WebApi/Dotnet.Sales.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Dotnet.Sales.WebApi"
RUN dotnet build "./Dotnet.Sales.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Dotnet.Sales.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Sales.WebApi.dll"]