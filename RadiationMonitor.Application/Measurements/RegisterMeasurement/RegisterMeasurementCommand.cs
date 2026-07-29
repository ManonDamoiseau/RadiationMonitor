using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Application.Measurements.RegisterMeasurement
{
    public class RegisterMeasurementCommand
    {
        public string DetectorId { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public double DoseRate { get; private set; }
        public DetectorStatus Status { get; private set; }

        public RegisterMeasurementCommand (string detectorId, DateTimeOffset timestamp, double doseRate, DetectorStatus status)
        {
            this.DetectorId = detectorId;
            this.Timestamp = timestamp;
            this.DoseRate = doseRate;
            this.Status = status;

        }
    }

}
