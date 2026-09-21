using Microsoft.EntityFrameworkCore;
using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Infrastructure.Measurements;
using RadiationMonitor.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Tests.Integration.Infrastructure
{
    public class EfCoreMeasurementRepositoryTests
    {
        private static RadiationMonitorDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<RadiationMonitorDbContext>()
                .UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=RadiationMonitor_Test;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            return new RadiationMonitorDbContext(options);
        }

        [Fact]
        public void Database_Should_Be_Available()
        {
            using var context = CreateContext();

            context.Database.EnsureCreated();

            Assert.True(context.Database.CanConnect());
        }
        [Fact]
        public void Add_Should_Persist_Measurement()
        {
            // Arrange
            var measurement = new Measurement(
                    "DET-TEST-001",
                    DateTime.UtcNow,
                    12.5,
                    DetectorStatus.Online
                    );

            using (var context = CreateContext())
            {
                context.Database.EnsureCreated();

                var repository = new EfCoreMeasurementRepository(context);

            // Act
                repository.Add(measurement);
            }

            // Assert
            using (var context = CreateContext())
            {
                var repository = new EfCoreMeasurementRepository(context);

                var result = repository.GetById(measurement.Id);

                Assert.NotNull(result);
                Assert.Equal(measurement.Id, result.Id);
                Assert.Equal(measurement.DetectorId, result.DetectorId);
                Assert.Equal(measurement.Timestamp, result.Timestamp);
                Assert.Equal(measurement.DoseRate, result.DoseRate);
                Assert.Equal(measurement.Status, result.Status);
            }
        }


    }
}
