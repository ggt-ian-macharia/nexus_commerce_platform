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
**Planned For:** Phase 2
**Purpose:** A free, open-source distributed application framework for .NET. We will use it to handle message bus communication (RabbitMQ/Azure Service Bus) easily.

### [Polly](https://www.thepollyproject.org/)
**Planned For:** Phase 4
**Purpose:** A .NET resilience and transient-fault-handling library. We will use it to implement retries, circuit breakers, and timeouts for inter-service HTTP calls.

### [Duende IdentityServer](https://duendesoftware.com/products/identityserver)
**Planned For:** Phase 4
**Purpose:** OpenID Connect and OAuth 2.0 framework for .NET. Will handle our centralized authentication and token issuance.
