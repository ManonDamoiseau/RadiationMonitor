using Microsoft.EntityFrameworkCore;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Infrastructure.Measurements
{
    public class EfCoreMeasurementRepository : IMeasurementRepository
    {
        private readonly RadiationMonitorDbContext _context;

        public EfCoreMeasurementRepository(RadiationMonitorDbContext context)
        {
            _context = context;
        }

        public void Add(Measurement measurement)
        {
            _context.Measurements.Add(measurement);
            _context.SaveChanges();
        }

        public Measurement? GetById(Guid id)
        {
            return _context.Measurements
                .FirstOrDefault(measurement => measurement.Id == id);
        }
    }
}
