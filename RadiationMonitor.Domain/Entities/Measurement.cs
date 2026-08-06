using RadiationMonitor.Domain.Enums;
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
                throw new ArgumentNullException(nameof(detectorId));
            }

            if (string.IsNullOrWhiteSpace(detectorId))
            {
                throw new ArgumentException("Detector id cannot be empty or whitespace.", nameof(detectorId));
            }

            if (timestamp == default(DateTimeOffset))
            {
                throw new ArgumentException("Incorrect Timestamp", nameof(timestamp));
            }
 
            if (doseRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(doseRate),"Dose rate is negative value");
            }

            if (status == DetectorStatus.Unknown)
            {
                throw new ArgumentException("Unknown status", nameof(status));
            }

            if (status == DetectorStatus.Error)
            {
                throw new ArgumentException("Error status", nameof(status));
            }

            this.Id = Guid.NewGuid();
            this.DetectorId = detectorId;
            this.Timestamp = timestamp;
            this.DoseRate = doseRate;
            this.Status = status;
        }

    };

   
}
