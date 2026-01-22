# Nexus Commerce Platform

A comprehensive microservices-based e-commerce platform built with .NET 8, demonstrating advanced distributed system patterns.

## Project Structure

The project follows a modular microservices architecture:

- **`src/ApiGateways`**: Entry points for client applications.
    - **`YarpGateway`**: A reverse proxy using [YARP](https://microsoft.github.io/reverse-proxy/) to route requests to backend services.
- **`src/Services`**: Autonomous business components.
    - **`Catalog`**: Manages products, categories, and inventory reference.
        - **`Catalog.API`**: The Web API project.
- **`src/BuildingBlocks`**: Shared libraries and cross-cutting concerns.
    - **`EventBus`**: Abstractions/implementations for asynchronous messaging (MassTransit/RabbitMQ).

### FAQ: Why the deep nesting?
You might notice paths like `src/Services/Catalog/Catalog.API`.
- **`src`**: Standard root for source code.
- **`Services`**: Groups all microservices.
- **`Catalog`**: The logical boundary for the Catalog domain. It holds everything related to Catalog.
- **`Catalog.API`**: The specific project that runs the HTTP API.
    - In the future, you might also have `Catalog.UnitTests`, `Catalog.IntegrationTests`, or `Catalog.Worker` inside the `Catalog` folder!

## Getting Started

### Prerequisites
- .NET 8 SDK
- Docker Desktop

### Running Locally
The easiest way to run the entire stack (Gateway, APIs, RabbitMQ, SQL Server) is via Docker Compose:

```bash
docker-compose up -d
```

### Running Individual Services
You can also run services directly with .NET:

```bash
dotnet run --project src/Services/Catalog/Catalog.API/Catalog.API.csproj
```

## Documentation
See the `docs/` folder for detailed guides:
- [Architecture](docs/architecture.md)
- [Roadmap](docs/roadmap.md)
- [CLI References](docs/reference/cli-commands.md)
