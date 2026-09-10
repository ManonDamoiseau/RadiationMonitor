using System;
using System.Collections.Generic;
using System.Text;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;

namespace RadiationMonitor.Infrastructure;

public class InMemoryMeasurementRepository : IMeasurementRepository
{
    private readonly List<Measurement> _measurements = new();

    public void Add(Measurement measurement)
    {
        _measurements.Add(measurement);
    }

    public Measurement? GetById (Guid id)
    {
        return _measurements.FirstOrDefault(measurement => measurement.Id == id);
    }
}
