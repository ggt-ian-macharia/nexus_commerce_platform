# .NET CLI Reference

This document tracks the .NET CLI commands used in creating and managing this microservices project.

## Solution & Project Management

### Create a new Solution
Creates an empty solution file (`.sln`) to organize projects.
```bash
dotnet new sln -n <SolutionName>
# Example:
dotnet new sln -n NexusCommerce
```

### Create a Web API Project
Creates a new ASP.NET Core Web API project (used for microservices).
```bash
dotnet new webapi -n <ProjectName>
# Example:
dotnet new webapi -n Catalog.API
```

### Create an Empty Web Project
Creates an empty ASP.NET Core web project (used for things like the lightweight Gateway).
```bash
dotnet new web -n <ProjectName>
# Example:
dotnet new web -n YarpGateway
```

### Add Project to Solution
Adds an existing project (`.csproj`) to the solution file.
```bash
dotnet sln add <PathToProject>
# Example:
dotnet sln add src/Services/Catalog/Catalog.API/Catalog.API.csproj
```

## Package Management

### Add a NuGet Package
Installs a NuGet package into a specific project.
```bash
dotnet add <PathToProject> package <PackageName>
# Example:
dotnet add src/ApiGateways/YarpGateway/YarpGateway.csproj package Yarp.ReverseProxy
```

### Add MassTransit (RabbitMQ)
Installs MassTransit with the RabbitMQ transport provider.
```bash
# For the EventBus shared library:
dotnet add src/BuildingBlocks/EventBus/EventBus.csproj package MassTransit.RabbitMQ

# For consuming services (if needed directly, though usually via EventBus):
dotnet add src/Services/Catalog/Catalog.API/Catalog.API.csproj package MassTransit.RabbitMQ
```

### Add Project Reference
Links one project to another (e.g., Catalog API needs EventBus).
```bash
dotnet add src/Services/Catalog/Catalog.API/Catalog.API.csproj reference src/BuildingBlocks/EventBus/EventBus.csproj
```

## Build & Run

### Build the Solution
Compiles all projects in the solution.
```bash
dotnet build
```

### Run a Project
Runs a specific project.
```bash
dotnet run --project <PathToProject>
```
