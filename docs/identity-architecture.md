# Identity & Authentication Architecture

This document outlines the progressive learning approach to identity and authentication in the Nexus Commerce Platform.

## Philosophy: Identity as a Learning Lab

Rather than treating authentication as "just another dependency to install," we use identity as a **forcing function** to learn professional security architecture patterns.

## Current Implementation: Simple JWT (Phase 1)

**What we have:**
- ASP.NET Core Identity for user management
- Custom JWT token generation
- Basic username/password authentication
- Role-based authorization (upcoming)

**Purpose:**
- Understand authentication fundamentals
- Learn token-based auth patterns
- Build clean architecture foundations
- Quick authentication for internal services

**When to use:**
- Internal microservice communication
- Simple mobile/SPA applications
- Rapid prototyping
- Learning baseline concepts

## Future: OAuth2/OIDC with Duende (Phase 4)

This is NOT about "adding a package" - it's about **architecting production-grade identity**.

### Learning Scenarios

#### Scenario 1: Multi-Client Authentication
**Goal:** Understand why different OAuth flows exist

**Implementation:**
- Web SPA → Authorization Code + PKCE flow
- Mobile App → Authorization Code + PKCE flow  
- Admin Dashboard → Different scopes and claims
- Public API → API keys + OAuth tokens

**What you'll learn:**
- Security implications of different client types
- Why PKCE exists (CSRF/code interception)
- Scope-based authorization
- Claims-based access control

#### Scenario 2: Service-to-Service Auth
**Goal:** Secure machine-to-machine communication

**Implementation:**
- Catalog → Inventory (Client Credentials flow)
- Order → Payment (Client Credentials with scopes)
- Gateway → Services (JWT bearer tokens)

**What you'll learn:**
- Client Credentials flow vs JWT
- Least privilege principle in practice
- Service identities vs user identities
- Trust boundaries in distributed systems

#### Scenario 3: External Identity Federation
**Goal:** Integrate external identity providers

**Implementation:**
- Login with Google
- Login with GitHub
- Login with Microsoft
- Map external claims to internal roles

**What you'll learn:**
- OpenID Connect federation
- Claims transformation
- Trust establishment
- User account linking strategies

#### Scenario 4: Production Security Patterns
**Goal:** Implement enterprise-grade security

**Implementation:**
- Token revocation strategy
- Signing key rotation (JWKS)
- Refresh token flow
- Rate limiting & brute force protection
- Audit logging

**What you'll learn:**
- Token lifecycle management
- Key rotation without downtime
- Compromise recovery strategies
- Security monitoring

## Architecture Comparison

### Simple JWT (Current)
```
User → Identity.API → Validate credentials
                   → Generate JWT
                   → Return token

Client → Service → Validate JWT signature
                → Extract claims
                → Authorize request
```

**Pros:**
- Simple to understand
- Direct control
- Fast implementation
- No external dependencies

**Cons:**
- No token revocation
- No refresh tokens
- No external identity providers
- Manual claim management

### OAuth2/OIDC (Future)
```
User → Duende → Authorization endpoint
             → Consent screen
             → Token endpoint
             → Return access_token + refresh_token + id_token

Client → Service → Validate token (introspection or JWT)
                → Check scopes
                → Authorize based on claims
```

**Pros:**
- Industry standard
- Token revocation
- External providers
- Advanced flows (delegation, impersonation)
- SSO capabilities

**Cons:**
- Complex to implement
- More moving parts
- Licensing considerations (Duende)
- Steeper learning curve

## When to Use What

### Use Simple JWT when:
- Internal microservices only
- Rapid development needed
- No external integrations
- Small team/organization
- Learning fundamentals

### Use OAuth2/OIDC when:
- Multiple client types (web, mobile, desktop)
- External identity providers needed
- Enterprise SSO required
- Third-party API access
- Complex authorization scenarios

## Implementation Roadmap

### Phase 1: Foundation ✅ (Current)
- [x] User management with ASP.NET Core Identity
- [x] JWT token generation
- [x] Basic authentication endpoints
- [ ] Role-based authorization
- [ ] User profile management

### Phase 2: Microservices Communication (Next)
- [ ] Service-to-service JWT validation
- [ ] Gateway authentication middleware
- [ ] Secure inter-service calls
- [ ] API key authentication for partners

### Phase 3: Enhanced Security
- [ ] Refresh token implementation
- [ ] Token blacklisting/revocation
- [ ] Rate limiting
- [ ] Audit logging
- [ ] Password policies & 2FA

### Phase 4: OAuth2/OIDC Lab
- [ ] Duende IdentityServer setup
- [ ] Authorization Code flow
- [ ] Client Credentials flow
- [ ] External provider integration
- [ ] Advanced claim mapping
- [ ] Token introspection
- [ ] Key rotation strategies

## Key Architecture Decisions

### Decision 1: Separate Auth Service
**Choice:** Dedicated Identity.API service

**Rationale:**
- Single source of truth for users
- Centralized authentication logic
- Independent scaling
- Clear security boundary

**Trade-offs:**
- Network hop for auth
- Single point of failure (mitigate with redundancy)

### Decision 2: JWT vs Opaque Tokens
**Choice:** JWT for now, both later

**Rationale:**
- JWT: Self-contained, no DB lookup, faster
- Opaque: Better revocation, smaller size

**Trade-offs:**
- JWT can't be easily revoked
- Will add token introspection later

### Decision 3: Duende vs Roll-Your-Own
**Choice:** Learn both approaches

**Rationale:**
- Simple JWT teaches fundamentals
- OAuth2 teaches production patterns
- Understanding both = better decisions

## Portfolio Value

This approach demonstrates:

✅ **Not just implementation** - architectural thinking
✅ **Trade-off analysis** - when to use each pattern  
✅ **Security depth** - token lifecycle, revocation, rotation
✅ **Production readiness** - enterprise patterns
✅ **Learning progression** - simple → complex

## Interview Talking Points

When discussing this project:

1. **"We started with simple JWT to learn fundamentals"**
   - Shows intentional learning path
   
2. **"Then evolved to OAuth2 to understand production patterns"**
   - Demonstrates growth mindset

3. **"Here's why we chose X over Y in this scenario"**
   - Shows decision-making ability

4. **"Here's how we'd handle token compromise"**
   - Security thinking

5. **"The trade-off between JWT and opaque tokens is..."**
   - Deep understanding

## Resources & Learning

- [OAuth 2.0 RFC 6749](https://tools.ietf.org/html/rfc6749)
- [OpenID Connect Core](https://openid.net/specs/openid-connect-core-1_0.html)
- [Duende IdentityServer Docs](https://docs.duendesoftware.com/)
- [JWT.io](https://jwt.io/)
- [OAuth 2.0 Playground](https://www.oauth.com/playground/)

---

**Next Steps:**
1. Complete current microservices (Catalog, Cart, Order)
2. Implement RBAC in Identity.API
3. Add service-to-service authentication
4. Begin OAuth2/OIDC learning scenarios
