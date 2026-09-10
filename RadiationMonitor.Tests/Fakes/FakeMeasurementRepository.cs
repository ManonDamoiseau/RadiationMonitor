using System;
using System.Collections.Generic;
using System.Text;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;

namespace RadiationMonitor.Tests.Fakes
{
    public class FakeMeasurementRepository : IMeasurementRepository
    {
        public List<Measurement> Measurements { get; } = new();

        public void Add(Measurement measurement) 
        {  
            Measurements.Add(measurement); 
        }

        public Measurement? GetById(Guid id)
        {
            return Measurements.FirstOrDefault(measurement => measurement.Id == id);
        }
    }
}
