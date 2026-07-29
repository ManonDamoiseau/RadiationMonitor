using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace RadiationMonitor.Tests
{
    public class RegisterMeasurementServiceTests
    {
        [Fact]
        public void RegisterMeasurement_Should_CreateMeasurement_WhenDataIsValid()
        {
            // Arrange
            RegisterMeasurementCommand command = new RegisterMeasurementCommand(
                "SN - 2026 - 0842B",
                DateTimeOffset.UtcNow,
                1.5,
                DetectorStatus.Online);
            RegisterMeasurementService service = new RegisterMeasurementService();

            //Act
            Measurement measurement = service.RegisterMeasurement(command);

            //Assert
            Assert.Equal(command.DetectorId, measurement.DetectorId);
            Assert.Equal(command.Timestamp, measurement.Timestamp);
            Assert.Equal(command.DoseRate, measurement.DoseRate);
            Assert.Equal(command.Status, measurement.Status);
        }
    }
}
