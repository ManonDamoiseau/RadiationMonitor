# Layered Architecture

## Overview
RadiationMonitor is organized into multiple layers. The objective is to isolate business rules from techical concerns.

## Architecture Diagram

```mermaid
flowchart TD 

	API[Presentation<br/>RadiationMonitor.API]

	APP[Application<br/>Use Cases]

	DOMAIN[Domain<br/>Business Rules]

	INFRA[Infrastructure<br/>Repositories]

	DB[(Database)]

	API --> APP
	APP --> DOMAIN
	INFRA --> DOMAIN
	INFRA --> DB
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