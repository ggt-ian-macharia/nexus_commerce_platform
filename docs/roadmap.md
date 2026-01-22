# Implementation Roadmap

This roadmap breaks down the construction of the Nexus Commerce Platform into 5 logical phases, each introducing new complexity and patterns.

## Phase 1: Foundation (Weeks 1-2)
**Goal:** Establish the spine of the system.
- [ ] Set up solution structure (sln, docker-compose).
- [ ] Create **API Gateway (YARP)**.
- [ ] Build **Catalog Service** (Simple CRUD).
- [ ] Connect Gateway to Catalog.
- [ ] Containerize services with Docker.

## Phase 2: Communication Infrastructure (Weeks 3-4)
**Goal:** Enable services to talk to each other.
- [ ] Set up **RabbitMQ** container.
- [ ] Implement generic Event Bus abstraction (MassTransit or native).
- [ ] Create **Cart Service** (Redis interaction).
- [ ] Implement request/response pattern via Generic Message.

## Phase 3: The Core & Sagas (Weeks 5-7)
**Goal:** Handle the complex business logic.
- [ ] Build **Order Service** foundation.
- [ ] Implement **Saga Orchestrator** for Order Fulfillment.
- [ ] Build **Inventory Service** with locking mechanisms.
- [ ] Build **Payment Service** stub.
- [ ] Wire up the complete "Place Order" flow.

## Phase 4: Reliability & Identity (Weeks 8-9)
**Goal:** Make it production-ready and secure.
- [ ] Implement **Duende IdentityServer**.
- [ ] Secure all APIs with JWT.
- [ ] Add **Polly** policies (Retries, Circuit Breakers) to Gateway and HTTP Clients.
- [ ] Implement Health Checks and Watchdog.

## Phase 5: Observability & Scale (Weeks 10+)
**Goal:** See what's happening inside.
- [ ] Add **OpenTelemetry** instrumentation.
- [ ] Set up **Seq** or **ELK Stack** for logging.
- [ ] Deploy to local **Kubernetes (k8s)** cluster.
- [ ] Implement **BFF** aggregation logic.
