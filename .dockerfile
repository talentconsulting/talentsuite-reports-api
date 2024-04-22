# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 443

# Build & publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY ./src ./src
WORKDIR ./src/TalentConsulting.TalentSuite.ReportsApi

ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./TalentConsulting.TalentSuite.ReportsApi.csproj -c $BUILD_CONFIGURATION -o /app/publish -p:UseAppHost=false

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
USER app
ENTRYPOINT ["dotnet", "TalentConsulting.TalentSuite.ReportsApi.dll"]