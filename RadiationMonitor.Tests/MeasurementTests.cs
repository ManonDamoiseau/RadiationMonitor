using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using System.Net.NetworkInformation;

namespace RadiationMonitor.Tests
{
    public class MeasurementTests
    {
        [Fact]
        public void Constructor_Should_CreateMeasurement_WhenDataIsValid()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
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

        [Fact]
        public void Constructor_Should_Throw_WhenDetectorIdIsNull()
        {
            //Arrange
            string detectorIdTest = null;
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));  
        }

        [Fact]
        public void Constructor_Should_Throw_WhenDetectorIdIsEmpty()
        {
            // Arrange
            string detectorIdTest = "";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));
        }

        [Fact]
        public void Constructor_Should_Throw_WhenDetectorIdIsWhitespace()
        {
            // Arrange
            string detectorIdTest = " ";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));
        }

        [Fact]
        public void Constructor_Should_Throw_WhenTimestampIsDefault()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = default(DateTimeOffset);
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));
        }

        [Fact]
        public void Constructor_Should_Throw_WhenDoseRateIsNegative()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = -1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act + Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));
        }

        [Fact]
        public void Constructor_Should_Throw_WhenStatusIsUnknown()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Unknown;

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));
        }

        [Fact]
        public void Constructor_Should_Throw_WhenStatusIsError()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Error;

            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));
        }
    }
}
