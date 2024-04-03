# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 443


# Copy Solution File to support Multi-Project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY TalentConsulting.TalentSuite.Reports.API.sln ./

# Copy Dependencies
COPY ["src/TalentConsulting.TalentSuite.Reports.API/TalentConsulting.TalentSuite.Reports.API.csproj", "src/TalentConsulting.TalentSuite.Reports.API/"]
COPY ["src/TalentConsulting.TalentSuite.Reports.Core/TalentConsulting.TalentSuite.Reports.Core.csproj", "src/TalentConsulting.TalentSuite.Reports.Core/"]
COPY ["src/TalentConsulting.TalentSuite.Reports.Infrastructure/TalentConsulting.TalentSuite.Reports.Infrastructure.csproj", "src/TalentConsulting.TalentSuite.Reports.Infrastructure.Infrastructure/"]
COPY ["src/TalentConsulting.TalentSuite.Reports.Common/TalentConsulting.TalentSuite.Reports.Common.csproj", "src/TalentConsulting.TalentSuite.Reports.Common/"]

# Restore Project
RUN dotnet restore "src/TalentConsulting.TalentSuite.Reports.API/TalentConsulting.TalentSuite.Reports.API.csproj"

# Copy Everything
COPY . .

# Build
WORKDIR "/src/src/TalentConsulting.TalentSuite.Reports.API"
RUN dotnet build "TalentConsulting.TalentSuite.Reports.API.csproj" -c Release -o /app/build

# publish
FROM build AS publish
WORKDIR "/src/src/TalentConsulting.TalentSuite.Reports.API"
RUN dotnet publish "TalentConsulting.TalentSuite.Reports.API.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentConsulting.TalentSuite.Reports.API.dll"]