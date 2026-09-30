using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Application.Measurements.GetMeasurement
{
    public class GetMeasurementService : IGetMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository;

        public GetMeasurementService(
            IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository;
        }

        public Measurement? GetMeasurement(
            GetMeasurementQuery query)
        {
            return _measurementRepository.GetById(query.Id);
        }
    }
}

