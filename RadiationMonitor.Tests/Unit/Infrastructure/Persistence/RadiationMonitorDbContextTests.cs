using Microsoft.EntityFrameworkCore;
using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Tests.Unit.Infrastructure.Persistence
{
    public class RadiationMonitorDbContextTests
    {
        [Fact]
        public void Model_Should_Contain_Measurement_Entity()
        {
            var options = new DbContextOptionsBuilder<RadiationMonitorDbContext>()
                .UseInMemoryDatabase("RadiationMonitorTests")
                .Options;

            using var context = new RadiationMonitorDbContext(options);

            var entityType = context.Model.FindEntityType(typeof(Measurement));

            Assert.NotNull(entityType);
        }

        [Fact]
        public void Model_Should_Configure_Measurement_Id_Correctly()
        {
            var options = new DbContextOptionsBuilder<RadiationMonitorDbContext>()
                .UseInMemoryDatabase("RadiationMonitorTests")
                .Options;

            using var context = new RadiationMonitorDbContext(options);

            var entityType = context.Model.FindEntityType(typeof(Measurement));

            var property = entityType?.FindProperty(
                nameof(Measurement.Id));

            var primaryKey = entityType?.FindPrimaryKey();

            Assert.NotNull(property);
            Assert.Equal(typeof(Guid), property.ClrType);

            Assert.NotNull(primaryKey);
            Assert.Contains(property, primaryKey.Properties);
        }

        [Fact]
        public void Measurement_Status_Should_Be_Stored_As_String()
        {
            var options = new DbContextOptionsBuilder<RadiationMonitorDbContext>()
                .UseInMemoryDatabase("RadiationMonitorTests")
                .Options;

            using var context = new RadiationMonitorDbContext(options);

            var entityType = context.Model.FindEntityType(typeof(Measurement));

            var property = entityType?.FindProperty(
                nameof(Measurement.Status));

            Assert.NotNull(property);
            Assert.Equal(typeof(DetectorStatus), property.ClrType);

            var typeMapping = property.GetTypeMapping();

            Assert.NotNull(typeMapping.Converter);
            Assert.Equal(typeof(string), typeMapping.Converter.ProviderClrType);
        }


    }
}
