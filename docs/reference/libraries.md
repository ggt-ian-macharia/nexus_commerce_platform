# Libraries & Packages

This document maintains a list of key libraries used in the Nexus Commerce Platform and their purpose.

## Core Infrastructure

### [Yarp.ReverseProxy](https://microsoft.github.io/reverse-proxy/)
**Used In:** `YarpGateway`
**Purpose:** A toolkit for building fast proxy servers in .NET. We use it as our API Gateway to route external requests to internal microservices. It handles:
- Request routing
- Load balancing
- Request/Response transformation
- Auth offloading (future)

## Upcoming Libraries (Planned)

### [MassTransit](https://masstransit.io/)
**Version:** 8.3.4
**Used In:** `EventBus` (Shared Library), `Catalog.API`
**Purpose:** a free, open-source distributed application framework for .NET. We use it to abstract away the complexity of RabbitMQ.
- **`MassTransit.RabbitMQ`**: The specific transport package for RabbitMQ.

### [AutoMapper](https://automapper.org/)
**Version:** 12.0.1
**Used In:** `Identity.API`
**Purpose:** Object-to-object mapping library that eliminates tedious manual mapping code. We use it to map between entities and DTOs.
- Configured via `Profile` classes (e.g., `AuthProfile`)

### [FluentValidation](https://fluentvalidation.net/)
**Version:** 11.3.0
**Used In:** `Identity.API`
**Purpose:** A popular validation library for .NET that uses a fluent interface and lambda expressions for building strongly-typed validation rules.
- Configured via `AbstractValidator<T>` classes (e.g., `RegisterRequestValidator`)

## Upcoming Libraries (Planned)

### [Polly](https://www.thepollyproject.org/)
**Planned For:** Phase 4
**Purpose:** A .NET resilience and transient-fault-handling library. We will use it to implement retries, circuit breakers, and timeouts for inter-service HTTP calls.

### [Duende IdentityServer](https://duendesoftware.com/products/identityserver)
**Planned For:** Phase 4
**Purpose:** OpenID Connect and OAuth 2.0 framework for .NET. Will handle our centralized authentication and token issuance.
