using System;
using System.Collections.Generic;
using System.Text;
using RadiationMonitor.Domain.Entities;

namespace RadiationMonitor.Application.Measurements.GetMeasurement
{
    public interface IGetMeasurementService
    {
        Measurement? GetMeasurement(GetMeasurementQuery query);
    }
}
