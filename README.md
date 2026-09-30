# RadiationMonitor

## Overview
RadiationMonitor is a personal software engineering learning project that simulates the ingestion and management of radiation measurements produced by radiation detectors.
The project is developed using practices commonly used in professional software projects, with a focus on maintainability, testing, documentation and incremental development.

## Learning Objectives
The project is used to practice:
- Layered architecture inspired by Clean Architecture principles
- Domain modeling and business validation
- Application use cases
- Unit and integration testing with xUnit
- Dependency injection
- Entity Framework Core and SQL Server persistence
- Git workflow
- Software documentation and Architecture Decision Records (ADR)
- CI/CD

## Architecture
The application follows a layered architecture inspired by Clean Architecture principles.
```
Presentation --> Application --> Domain
                    |
                    v
          IMeasurementRepository
                    ^
                    |
              Infrastructure
```

The main responsibilities are:
- API: exposes application capabilities through external interfaces such as REST and, later, gRPC.
- Application: implements use cases and coordinates application workflows.
- Domain: contains the domain model and business rules.
- Infrastructure: provides technical implementations such as persistence and database access.
Dependencies are directed toward abstractions and the domain rather than toward infrastructure-specific implementations.

## Solution Structure
- RadiationMonitor.API : ASP.NET Core Web API exposing the application's use cases
- RadiationMonitor.Application : Coordinates the application's use cases
- RadiationMonitor.DetectorSimulator : Simulates radiation detector data sent to the application
- RadiationMonitor.Domain : Business entities, domain rules and value validation
- RadiationMonitor.Infrastructure : Technical implementations, repositories and database access
- RadiationMonitor.Tests : Unit tests and integration tests

## Current Features
The following features are currently implemented:
- Measurement domain model
- Domain-level business validation
- Register Measurement use case
- Get Measurement use case
- IMeasurementRepository abstraction
- Entity Framework Core repository implementation
- Dependency injection configuration
- Entity Framework Core integration
- SQL Server LocalDB persistence
- Initial Entity Framework Core migration
- Database creation and updates through EF Core migrations
- REST API for registering measurements
- REST API for retrieving measurements by ID
- API integration tests for the measurement endpoints
- Domain unit tests
- Application unit tests
- Infrastructure tests for EF Core model configuration
- Persistence validation through integration tests

## Technologies
The project currently uses : 
- C#
- .NET
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- xUnit
- Git
- Markdown documentation

## Getting Started

### Prerequisites
- .NET
- Visual Studio 2026 Community (or another compatible IDE)
- SQL Server LocalDB

### Build
Clone the repository, open the solution and build it with : dotnet build

## Run the API
Run the RadiationMonitor.API project from Visual Studio or with the .NET CLI.
Database creation and updates are handled through Entity Framework Core migrations according to the application's configuration.

## Running Tests
Run the complete test suite with : dotnet test

The test suite currently covers:
- Domain business validation
- Application use cases
- API integration tests
- Entity Framework Core configuration
- SQL Server LocalDB persistence

## API
The REST API currently exposes measurement operations : 
- Register a measurement : POST /api/Measurements
- Get a measurement : GET /api/Measurements/{id}

The API currently returns the following HTTP status codes :
- 201 Created : when a measurement is successfully registered
- 200 OK : when a requested measurement is found
- 400 Bad Request : when the measurement is invalid
- 404 Not Found : when the requested measurement does not exist

## Documentation
Additional documentation is available in the "docs" folder.

```
docs/
|_use-cases/
|_domain/
|_architecture/
|_adr/


```
The documentation includes:
- Application use cases
- Domain model and business concepts
- Architecture documentation
- Architecture Decision Records (ADR)

### ADR
- ADR-0001 - Adopt a Layered Architecture Inspired by Clean Architecture
- ADR-0002 - Persist DetectorStatus as String

## Roadmap

### Completed
- [x] Domain model
- [x] Business validation
- [x] Register Measurement use case
- [x] In-memory repository
- [x] Dependency Injection configuration
- [x] Entity Framework Core integration
- [x] SQL Server persistence
- [x] Measurement REST API

### Planned
- [ ] gRPC service
- [ ] Detector simulator
- [ ] CI/CD pipeline
- [ ] Docker support



