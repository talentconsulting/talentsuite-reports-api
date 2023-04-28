# talentsuite-reports-api

The Talent Consulting Report API exposes information relating to reports, the data of which is held in its underlying SQL database.


## How It Works

The API is a simple .Net Core restful API with a SQL Server backend.


## 🚀 Installation

### Pre-Requisites

* A clone of this repository
* A code editor that supports .NetCore 6
* A SQL Server installation
* Storage emulator
* Database project published to local db server

### Config

This service uses the standard Talent Consulting configuration.

Configuration should be added to resemble the following:

```
{
  "UseDbType": "UseInMemoryDatabase",
  //"UseDbType": "UsePostgresDatabase",
  "LogLevel": "Verbose",
  "ConnectionStrings": {
    //"ReferralConnection": "Server={Your Server};Database={Your Database};Trusted_Connection=True;MultipleActiveResultSets=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```

## Migrations

To Add Migration

<br />
 dotnet ef migrations add CreateIntialSchema 
<br />

To Apply Latest Schema Manually

<br />
 dotnet ef database update 
<br />


## 🔗 External Dependencies

Talent Consulting Report API has no external dependenices.

## Technologies

* .NetCore 6
* Web API
* SQL Server
* Entity Framework
* NLog
* Azure Table Storage
* NUnit
* Moq
* FluentAssertions
