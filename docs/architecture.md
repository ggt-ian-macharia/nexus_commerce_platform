# Architecture & Design: Nexus Commerce Platform

## 1. System Overview
The system follows a domain-driven, microservices-based architecture. A Backend-for-Frontend (BFF) pattern via an API Gateway handles client requests, routing them to appropriate backend services.

### High-Level Diagram
```mermaid
graph TD
    Client[Web/Mobile Client] --> Gateway[API Gateway (YARP)]
    
    subgraph "Core Services"
        Gateway --> Identity[Identity Service]
        Gateway --> Catalog[Catalog Service]
        Gateway --> Cart[Cart Service]
        Gateway --> Order[Order Service]
        Gateway --> Payment[Payment Service]
    end

    subgraph "Support Services"
        Order -.-> Inventory[Inventory Service]
        Order -.-> Shipping[Shipping Service]
        Order -.-> Pricing[Pricing Service]
    end

    subgraph "Infrastructure"
        EventBus[Event Bus (RabbitMQ)]
        Identity -.-> EventBus
        Order -.-> EventBus
        Payment -.-> EventBus
        Inventory -.-> EventBus
    end
```

## 2. Service Catalogue

| Service | Responsibility | Database | Patterns | Status |
|---------|----------------|----------|----------|--------|
| **Identity** | AuthN/AuthZ, Token Issuance, User Profiles | PostgreSQL | ASP.NET Core Identity, JWT | ✅ Implemented |
| **Catalog** | Products, Categories, Brands | PostgreSQL | CQRS, Event Publishing | ✅ Implemented |
| **Cart** | Shopping Cart, Session State | Redis | Key-Value Store, TTL | ✅ Implemented |
| **Notification** | Email/SMS Notifications, Event Consumer | N/A (Stateless) | Event-Driven, Consumer Pattern | ✅ Implemented |
| **Order** | Order Lifecycle, Saga Orchestration | PostgreSQL | Event Sourcing, Saga, Outbox | 🚧 In Progress |
| **Payment** | Payment Processing, Refund Handling | MongoDB | Adapter Pattern, Idempotency | 📋 Planned |
| **Inventory**| Stock Levels, Reservations | PostgreSQL | Distributed Locking, Optimistic Concurrency | 📋 Planned |
| **Search** | Product Search, Filtering, Facets | ElasticSearch | Event-carried State Transfer | 📋 Planned |

## 3. Communication Strategy

### Synchronous (Command)
- **gRPC**: Used for high-performance internal service-to-service calls (e.g., Aggurator calling Catalog).
- **REST (HTTP/2)**: Standard external API exposure.

### Asynchronous (Event)
- **Message Broker**: RabbitMQ (Dev) / Azure Service Bus (Prod).
- **Events**:
  - `OrderCreated`
  - `PaymentProcessed`
  - `StockReserved`
  - `ShipmentDispatched`

## 4. Resilience Patterns
Implemented using **Polly**:
- **Retry**: Exponential backoff for transient failures.
- **Circuit Breaker**: Fail fast when dependent services are down.
- **Bulkhead**: Isolate critical resources.
- **Timeout**: Prevent cascading latency.

## 5. Security Architecture
- **Duende IdentityServer**: Centralized token issuer.
- **Traffic Encryption**: TLS 1.3 everywhere.
- **Secrets**: Azure Key Vault integration.
- **Network Policy**: Zero-trust approach within Kubernetes.

## 6. Infrastructure Layout
- **Containerization**: Docker multi-stage builds.
- **Orchestration**: Kubernetes (AKS/EKS).
- **Ingress**: NGINX Ingress Controller.
- **Service Mesh**: Linkerd (optional phase).

## 7. Development Standards
- **Clean Architecture** for internal service structure.
- **Vertical Slice** for simpler CRUD services.
- **Conventional Commits** for git history.
- **Semantic Versioning** for API versions.
