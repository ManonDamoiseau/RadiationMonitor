# ADR-0002 - Persist DetectorStatus as String

## Status
Accepted

## Context
Measurement contains a Status property represented by the DetectorStatus enum in the domain model.
By convention, Entity Framework Core persists enum properties as their underlying numeric value.

This creates a persistence concern because the meaning of the stored value depends on the numeric values assigned to the enum members.
A change to the enum declaration could therefore change the meaning of existing persisted data.

## Decision
DetectorStatus will remain an enum in the domain model but will be persisted as a string in the database.
Entity Framework Core will explicitly configure a value conversion from DetectorStatus to string.
The resulting persistence representation will therefore be based on the enum member name.

## Consequences

### Advantages

- Persisted values are human-readable.
- The database does not depend on the numeric ordering of enum members.
- Inspecting stored measurements is easier because the status value has an explicit meaning.

### Trade-offs
- String values consume more storage than numeric enum values.
- The mapping is more explicit than EF Core's default enum mapping.