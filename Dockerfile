FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Technical-Test-COTO/Technical-Test-COTO.csproj Technical-Test-COTO/
COPY Technical-Test-COTO/Technical-Test-COTO.sln    Technical-Test-COTO/
COPY Application/Application.csproj                 Application/
COPY Domain/Domain.csproj                           Domain/
COPY Infrastructure/Infrastructure.csproj           Infrastructure/
COPY Application.Tests/Application.Tests.csproj     Application.Tests/

RUN dotnet restore Technical-Test-COTO/Technical-Test-COTO.sln
COPY . .

RUN dotnet build Technical-Test-COTO/Technical-Test-COTO.sln -c $BUILD_CONFIGURATION

RUN dotnet test Application.Tests/Application.Tests.csproj -c $BUILD_CONFIGURATION --no-build --logger "trx;LogFileName=test_results.trx"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Technical-Test-COTO/Technical-Test-COTO.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Technical-Test-COTO.dll"]
