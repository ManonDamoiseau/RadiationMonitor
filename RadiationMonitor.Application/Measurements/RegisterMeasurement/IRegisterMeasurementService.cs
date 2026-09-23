using RadiationMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Application.Measurements.RegisterMeasurement
{
    public interface IRegisterMeasurementService
    {
        Measurement RegisterMeasurement(RegisterMeasurementCommand command);
    }
}
