using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using System.Net.NetworkInformation;

namespace RadiationMonitor.Tests
{
    public class MeasurementTests
    {
        [Fact]
        public void CreateMeasurement_WithValidData_ShouldInitializeProperties()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTime.Today;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act
            Measurement measure1 = new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest);

            //Assert

            Assert.NotEqual(Guid.Empty, measure1.Id);
            Assert.Equal(detectorIdTest, measure1.DetectorId);
            Assert.Equal(timestampTest, measure1.Timestamp);
            Assert.Equal(doseRateTest, measure1.DoseRate);
            Assert.Equal(statusTest, measure1.Status);
        }
    }
}
