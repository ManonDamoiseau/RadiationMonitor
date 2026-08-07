# RadiationMonitor

## Overview
RadiationMonitor is a learning project designed to simulate the receipt and management of radiation measurements produced by medical equipment.

## Objectives
The aim of this project is to develop an application using a software engineering approach and to create a project that is maintainable, 
tested and documented in the same way as a professional project.

Current learning objectives includes:
- Layered architecture
- Unit testing with xUnit
- Git workflow
- Software documentation (README and ADR)
- CI/CD preparation

## Architecture
The architecture fort this application is inspired by Clean Architecture principles.
```
Presentation --> Application --> Domain <-- Infrastructure
```

Each layer has a single responsibility:
- Presentation : receive requests from users or external systems (API, UI, gRPC, ...).
- Application : orchestrates use cases and coordinates the workflow.
- Domain : contains business rules and domain model.
- Infrastructure : implements technical concerns such as data persistence or external services.

## Solution Structure
- RadiationMonitor.API : ASP.NET Core Web API exposing the application's use cases
- RadiationMonitor.Application : Coordinates the application's use cases
- RadiationMonitor.DetectorSimulator : Simulates radiation detector data sent to the application
- RadiationMonitor.Domain : Business entities, domain rules and value validation
- RadiationMonitor.Infrastructure : Technical implementations, repositories and database access
- RadiationMonitor.Tests : Unit tests for the Domain and Application layers

## Current Features
The features currently implemented :
- Domain model for Measurement
- Business validation inside the domain entity
- Register Measurement use case
- Repository abstraction (IMeasurementRepository)
- Unit tests for the Domain layer
- Unit tests for the Application layer : initialized

## Technologies
The project currently uses : 
- C#
- .NET
- ASP.NET Core Web API
- xUnit
- Git
- Markdown documentation

## Getting Started

### Prequisities
- .NET
- Visual Studio 2026 Community (or another compatible IDE)

### Build
Clone the repository and open the solution : dotnet build

Run the API project from Visual Studio or using the .NET CLI.

## Running Tests
The project uses xUnit for unit testing

Run all tests with : dotnet test

Current test coverage focuses on:
- Domain validation
- Register Measurement use case

## Documentation
Additional documentation is available in the "docs" folder.
- Architecture overview
- Architecture Decision Records (ADR)
- Domain model description
- Use case documentation

```
docs/
|
|_use-cases/
|	|_Application workflow documentation
|
|_domain/
|	|_Business concepts and domain model
|
|_architecture/
|	|_System architecture documentation
|
|_adr/
|	|_Architecture decisions
|
|_README.md

```

### ADR
- ADR-0001 - Adopt a Layered Architecture Inspired by Clean Architecture

## Roadmap
- [x] Domain model
- [x] Business validation
- [x] Register Measurement use case
- [ ] In-memory repository
- [ ] Dependendcy Injection configuration
- [ ] Entity Framework Core integration
- [ ] SQL Server persistence
- [ ] Measurement REST API
- [ ] gRPC service
- [ ] Detector simulator
- [ ] CI/CD pipeline
- [ ] Docker support

