# ADR-0001 - Adopt a Layered Architecture Inspired by Clean Architecture

## Status
Accepted

## Context
RadiationMonitor is designed as a learning project with professional software engineering practices in mind.
The application is expected to evolve over time by adding new use cases, persistence mechanisms, APIs and automated tests.

Using a single project mixing business rules, application logic and technical concerns would make the code harder to maintain, test and evolve.
A clear separation of responsibilities is therefore required.

## Decision
The solution is organised into four logical layers:

- Domain : contains the business model and business rules.
- Application : implements use cases and orchestrates domain objects.
- Infrastructure : contains technical implementations such as repositories and external services.
- Presentation (ASP.NET Core Web API) : exposes the application through HTTP endpoints.

Dependencies always point toward the business domain. The architecture follows the principles of 
Clean Architecture while remaining pragmatic for the size of the project.

## Consequences

### Advantages

- Business rules remain independent from technical frameworks.
- The domain can be tested in isolation.
- Infrastructure can evolve without impacting business logic.
- Responsibilities are clearly separated.
- The architecture remains scalable as new features are added.

### Trade-offs
More projects and abstractions increase the initial complexity.