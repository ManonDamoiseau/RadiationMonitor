# Layered Architecture

## Overview
RadiationMonitor is organized into multiple layers. The objective is to isolate business rules from techical concerns.

## Architecture Diagram

```mermaid
flowchart TD 

    API[RadiationMonitor.API<br/>Presentation]

    APP[RadiationMonitor.Application<br/>Use Cases]

    DOMAIN[RadiationMonitor.Domain<br/>Business Rules]

    REPO[IMeasurementRepository<br/>Repository Abstraction]

    INFRA[RadiationMonitor.Infrastructure<br/>Technical Implementations]

    INMEM[InMemoryMeasurementRepository<br/>In-Memory Repository]

    TESTS[RadiationMonitor.Tests]

    API --> APP

    APP --> DOMAIN
    APP --> REPO

    INFRA --> REPO
    INMEM --> REPO

    TESTS --> DOMAIN
    TESTS --> APP

```

## Reponsibilities

### Presentation
- Receive external requests
- Expose application features
- Communication with external clients

### Application 
- Implement use cases
- Coordinate application workflow

### Domain
- Business entities
- Business rules
- Validation

### Infrastructure
- Data persistence
- External systems
- Technical implementations
- Concrete implementation of application-defined repository contracts

The current implementation includes InMemoryMeasurementRepository, which stores measurements in memory and does not provide durable persistence.