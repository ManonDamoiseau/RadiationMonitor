using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace RadiationMonitor.Tests.Infrastructure
{
    public class InMemoryMeasurementRepositoryTests
    {
        [Fact]
        public void GetById_Should_ReturnMeasurement_WhenIdExists()
        {
            //Arrange
            Measurement measurement = new Measurement
            (
                "SN - 2026 - 0842B",
                DateTimeOffset.UtcNow,
                1.5,
                DetectorStatus.Online
            );
            InMemoryMeasurementRepository repository = new();

            //Act
            repository.Add(measurement);
            Measurement? result = repository.GetById(measurement.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(measurement.Id, result.Id);
        }

        [Fact]
        public void GetById_Should_ReturnNull_WhenIdDoesNotExist()
        {
            //Arrange
            InMemoryMeasurementRepository repository = new();

            //Act
            Measurement? result = repository.GetById(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }
    }
}
