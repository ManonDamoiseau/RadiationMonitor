using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;


namespace RadiationMonitor.Application.Measurements.RegisterMeasurement
{
    public class RegisterMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository; 

        public RegisterMeasurementService(IMeasurementRepository measurementRepository) 
        {
            _measurementRepository = measurementRepository; 
        }

        public Measurement RegisterMeasurement(RegisterMeasurementCommand command) 
        {
            Measurement measurement = new Measurement(
            command.DetectorId,
            command.Timestamp,
            command.DoseRate,
            command.Status
            ); 

            _measurementRepository.Add(measurement); 
            
            return measurement;

        }

        

    }
}
