# Nexus Commerce Platform

A comprehensive microservices-based e-commerce platform built with .NET 9, demonstrating advanced distributed system patterns.

## Project Structure

The project follows a modular microservices architecture:

- **`src/ApiGateways`**: Entry points for client applications.
    - **`YarpGateway`**: A reverse proxy using [YARP](https://microsoft.github.io/reverse-proxy/) to route requests to backend services.
- **`src/Services`**: Autonomous business components.
    - **`Catalog`**: Manages products, categories, and inventory reference.
    - **`Cart`**: Shopping cart with Redis-based session storage.
    - **`Order`**: Order management and orchestration.
    - **`Identity`**: Handles authentication and authorization using ASP.NET Core Identity.
    - **`Notification`**: Event-driven notification service for system events.
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
- .NET 9 SDK
- Docker Desktop
- PostgreSQL (for Catalog, Identity, Order services)
- Redis (for Cart service)
- RabbitMQ (for event bus)

### Running Locally
The easiest way to run the entire stack (Gateway, APIs, RabbitMQ, Redis) is via Docker Compose:

```bash
docker compose up -d
```

**Services will be available at:**
- API Gateway: `http://localhost:8080`
- Catalog API: `http://localhost:5000` (direct access)
- Identity API: `http://localhost:5001` (direct access)
- Notification API: `http://localhost:5002` (direct access)
- Cart API: `http://localhost:5003` (direct access)
- Order API: `http://localhost:5004` (direct access)
- RabbitMQ Management: `http://localhost:15672` (guest/guest)

### Running Individual Services
You can also run services directly with .NET:

```bash
# Identity Service
dotnet run --project src/Services/Identity/Identity.API/Identity.API.csproj

# Catalog Service
dotnet run --project src/Services/Catalog/Catalog.API/Catalog.API.csproj
```

## Authentication

The Identity service provides JWT-based authentication. Use the Gateway endpoints:

**Register:**
```bash
POST http://localhost:8080/api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Test123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

**Login:**
```bash
POST http://localhost:8080/api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Test123!"
}
```

The response includes a JWT token to use in the `Authorization: Bearer <token>` header.

## Documentation
See the `docs/` folder for detailed guides:
- [Architecture](docs/architecture.md)
- [Roadmap](docs/roadmap.md)
- [Identity Architecture & Learning Path](docs/identity-architecture.md)
- [CLI References](docs/reference/cli-commands.md)
- [Identity Setup](docs/reference/identity-setup.md)
- [RabbitMQ Setup](docs/reference/setup-rabbitmq.md)
