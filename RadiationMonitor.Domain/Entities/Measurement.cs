using RadiationMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RadiationMonitor.Domain.Entities
{
    /// <summary>
    /// Measurements sent by detector
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
            if (string.IsNullOrWhiteSpace(detectorId))
            {
                throw new ArgumentException("Detector id is missing");
            }
            if (timestamp == default(DateTimeOffset))
            {
                throw new ArgumentException("Incorrect Timestamp");
            }
 
            if (doseRate < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(doseRate),"Dose rate is negative value");
            }

            this.Id = Guid.NewGuid();
            this.DetectorId = detectorId;
            this.Timestamp = timestamp;
            this.DoseRate = doseRate;
            this.Status = status;
        }

    };

   
}
