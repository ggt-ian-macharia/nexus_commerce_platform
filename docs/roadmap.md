# Implementation Roadmap

This roadmap breaks down the construction of the Nexus Commerce Platform into 5 logical phases, each introducing new complexity and patterns.

## Phase 1: Foundation (Weeks 1-2) ✅ COMPLETED
**Goal:** Establish the spine of the system.
- [x] Set up solution structure (sln, docker-compose).
- [x] Create **API Gateway (YARP)**.
- [x] Build **Catalog Service** (Simple CRUD).
- [x] Connect Gateway to Catalog.
- [x] Containerize services with Docker.

## Phase 2: Communication Infrastructure (Weeks 3-4) ✅ COMPLETED
**Goal:** Enable services to talk to each other.
- [x] Set up **RabbitMQ** container.
- [x] Implement generic Event Bus abstraction (MassTransit).
- [x] Create **Cart Service** (Redis interaction).
- [x] Create **Notification Service** (Event consumer).
- [x] Implement event-driven architecture with publish/subscribe pattern.

## Phase 2.5: Core Microservices Patterns (CURRENT PRIORITY) 🚀
**Goal:** Implement production-ready resilience and reliability patterns.
- [ ] Build **Order Service** foundation (PostgreSQL, event publishing).
- [ ] Add **Health Checks** (liveness/readiness) to all services.
- [ ] Implement **Circuit Breakers** (Polly) in HTTP clients and Gateway.
- [ ] Add **Rate Limiting** to YARP Gateway.
- [ ] Implement **Simple Saga** (Order → Inventory Check → Payment stub using MassTransit state machines).

## Phase 3: Advanced Sagas & Business Services (Weeks 5-7)
**Goal:** Handle complex business logic with choreography.
- [ ] Build **Inventory Service** with locking mechanisms.
- [ ] Build **Payment Service** stub.
- [ ] Implement **Saga Orchestrator** for Order Fulfillment (Order → Inventory → Payment).
- [ ] Wire up the complete "Place Order" flow with compensating transactions.

## Phase 4: Identity & Security (Weeks 8-9)
**Goal:** Make it production-ready and secure.
- [x] Implement **ASP.NET Core Identity** for authentication.
- [ ] Secure all APIs with JWT tokens.
- [ ] Add API key authentication for service-to-service calls.
- [ ] Implement authorization policies (role-based, claims-based).

## Phase 5: Observability & Scale (Weeks 10+)
**Goal:** See what's happening inside.
- [ ] Add **OpenTelemetry** instrumentation.
- [ ] Set up **Seq** or **ELK Stack** for logging.
- [ ] Deploy to local **Kubernetes (k8s)** cluster.
- [ ] Implement **BFF** aggregation logic.
