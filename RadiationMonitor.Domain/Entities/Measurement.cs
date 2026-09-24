using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Domain.Entities
{
    /// <summary>
    /// Represents a validated radiation measurement produced by a detector.
    /// </summary>
    public class Measurement
    {
        public Guid Id { get; private set; }
        public string DetectorId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public double DoseRate { get; private set; }
        public DetectorStatus Status { get; private set; }

        public Measurement(string detectorId, DateTimeOffset timestamp, double doseRate, DetectorStatus status)
        {
            if (detectorId is null)
            {
                throw new InvalidMeasurementException(
                    nameof(DetectorId),
                    "Detector ID cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(detectorId))
            {
                throw new InvalidMeasurementException(
                    nameof(DetectorId),
                    "Detector ID cannot be empty or whitespace.");
            }

            if (timestamp == default(DateTimeOffset))
            {
                throw new InvalidMeasurementException(
                    nameof(Timestamp),
                    "Timestamp cannot be the default value.");
            }
 
            if (doseRate < 0)
            {
                throw new InvalidMeasurementException(
                    nameof(DoseRate),
                    "Dose rate cannot be negative.");
            }

            if (status == DetectorStatus.Unknown)
            {
                throw new InvalidMeasurementException(
                    nameof(Status),
                    "Unknown status is not allowed.");
            }

            if (status == DetectorStatus.Error)
            {
                throw new InvalidMeasurementException(
                    nameof(Status),
                    "Error status is not allowed.");
            }

            this.Id = Guid.NewGuid();
            this.DetectorId = detectorId;
            this.Timestamp = timestamp;
            this.DoseRate = doseRate;
            this.Status = status;
        }

    }; 
}
