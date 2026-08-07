using System;
using System.Collections.Generic;
using System.Text;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Entities;

namespace RadiationMonitor.Application.Measurements.RegisterMeasurement
{
    public interface IMeasurementRepository
    {
        void Add(Measurement measurement);
    }
}
