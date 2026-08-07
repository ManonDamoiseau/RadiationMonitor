# DetectorStatus

## Purpose
DetectorStatus represents the operational state of a radiation detector at the time a measurement is acquired.
The status provides additional business context and determines whether a measurement can be accepted by the domain.

## Values

| Value | Description |
|--------|-------------|
| Unknown | The default state or state is not known. This status is not accepted for a valid measurement. |
| Online | Operating normally and measurements can be accepted. |
| Warning | Abnormal condition has been detected and requires monitoring. Measurements are still accepted. |
| Error | The detector is in an error state. Measurements are rejected by the domain. |

## Business Rules
The domain currently accepts measurements only when the detector status is:

- Online
- Warning

Measurements with the following statuses are rejected:

- Unknown
- Error

These rules are enforced by the Measurement entity to guarantee domain consistency.