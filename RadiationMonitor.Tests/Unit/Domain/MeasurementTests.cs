using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Exceptions;
using System.Net.NetworkInformation;

namespace RadiationMonitor.Tests.Unit.Domain
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

            //Act
            InvalidMeasurementException exception = Assert.Throws<InvalidMeasurementException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));

            //Assert
            Assert.Equal(nameof(Measurement.DetectorId), exception.PropertyName);
            Assert.Equal("Detector ID cannot be null.", exception.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_Should_Throw_WhenDetectorIdIsEmptyOrWhitespace(
    string detectorIdTest)
        {
            // Arrange
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            // Act
            InvalidMeasurementException exception = Assert.Throws<InvalidMeasurementException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));

            // Assert
            Assert.Equal(nameof(Measurement.DetectorId), exception.PropertyName);
            Assert.Equal("Detector ID cannot be empty or whitespace.", exception.Message);
        }

        [Fact]
        public void Constructor_Should_Throw_WhenTimestampIsDefault()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = default(DateTimeOffset);
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act
            InvalidMeasurementException exception = Assert.Throws<InvalidMeasurementException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));

            //Assert
            Assert.Equal(nameof(Measurement.Timestamp), exception.PropertyName);
            Assert.Equal("Timestamp cannot be the default value.", exception.Message);
        }

        [Fact]
        public void Constructor_Should_Throw_WhenDoseRateIsNegative()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = -1.5;
            DetectorStatus statusTest = DetectorStatus.Online;

            //Act
            InvalidMeasurementException exception = Assert.Throws<InvalidMeasurementException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));

            //Assert
            Assert.Equal(nameof(Measurement.DoseRate), exception.PropertyName);
            Assert.Equal("Dose rate cannot be negative.", exception.Message);
        }

        [Fact]
        public void Constructor_Should_Throw_WhenStatusIsUnknown()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Unknown;

            //Act
            InvalidMeasurementException exception = Assert.Throws<InvalidMeasurementException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));

            //Assert
            Assert.Equal(nameof(Measurement.Status), exception.PropertyName);
            Assert.Equal("Unknown status is not allowed.", exception.Message);
        }

        [Fact]
        public void Constructor_Should_Throw_WhenStatusIsError()
        {
            // Arrange
            string detectorIdTest = "SN - 2026 - 0842B";
            DateTimeOffset timestampTest = DateTimeOffset.UtcNow;
            double doseRateTest = 1.5;
            DetectorStatus statusTest = DetectorStatus.Error;

            //Act
            InvalidMeasurementException exception = Assert.Throws<InvalidMeasurementException>(() =>
                new Measurement(detectorIdTest, timestampTest, doseRateTest, statusTest));

            //Assert
            Assert.Equal(nameof(Measurement.Status), exception.PropertyName);
            Assert.Equal("Error status is not allowed.", exception.Message);
        }
    }
}
