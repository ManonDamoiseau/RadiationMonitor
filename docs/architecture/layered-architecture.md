# Layered Architecture

## Overview
RadiationMonitor is organized into multiple layers. The objective is to isolate business rules from techical concerns.

## Architecture Diagram

```mermaid
flowchart TD 

    API[RadiationMonitor.API<br/>Presentation]

    APP[RadiationMonitor.Application<br/>Use Cases]

    DOMAIN[RadiationMonitor.Domain<br/>Business Rules]

    INFRA[RadiationMonitor.Infrastructure<br/>Repository Implementations]

    REPO[IMeasurementRepository<br/>Abstraction]

    TESTS[RadiationMonitor.Tests]

    API --> APP

    APP --> DOMAIN

    APP --> REPO

    INFRA --> REPO

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