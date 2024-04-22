# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 443


# Copy Solution File to support Multi-Project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src/TalentConsulting.TalentSuite.ReportsApi.sln ./

# Copy Dependencies
COPY ["src/TalentConsulting.MinimalApi.Registration/TalentConsulting.MinimalApi.Registration.csproj", "src/TalentConsulting.MinimalApi.Registration/"]
COPY ["src/TalentConsulting.TalentSuite.ReportsApi/TalentConsulting.TalentSuite.ReportsApi.csproj", "src/TalentConsulting.TalentSuite.ReportsApi/"]

# Restore Project
RUN dotnet restore "src/TalentConsulting.TalentSuite.ReportsApi/TalentConsulting.TalentSuite.ReportsApi.csproj"

# Copy Everything
COPY . .

# Build
WORKDIR "/src/src/TalentConsulting.TalentSuite.ReportsApi"
RUN dotnet build "TalentConsulting.TalentSuite.ReportsApi.csproj" -c Release -o /app/build

# publish
FROM build AS publish
WORKDIR "/src/src/TalentConsulting.TalentSuite.ReportsApi"
RUN dotnet publish "TalentConsulting.TalentSuite.ReportsApi.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER app
ENTRYPOINT ["dotnet", "TalentConsulting.TalentSuite.ReportsApi.dll"]