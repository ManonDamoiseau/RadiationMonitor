using Grpc.Core;
using Moq;
using RadiationMonitor.API.Grpc;
using RadiationMonitor.API.Protos;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Tests.Unit.API
{
    public class MeasurementGrpcServiceTests
    {
        [Fact]
        public async Task RegisterMeasurement_WithValidRequest_ReturnsMeasurementId()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;

            var measurement = new Measurement(
                "DETECTOR-01",
                timestamp,
                12.5,
                RadiationMonitor.Domain.Enums.DetectorStatus.Online);

            var serviceMock =
                new Mock<IRegisterMeasurementService>();

            serviceMock
                .Setup(s => s.RegisterMeasurement(
                    It.Is<RegisterMeasurementCommand>(command =>
                        command.DetectorId == "DETECTOR-01" &&
                        command.Timestamp == timestamp &&
                        command.DoseRate == 12.5 &&
                        command.Status ==
                            RadiationMonitor.Domain.Enums.DetectorStatus.Online)))
                .Returns(measurement);

            var service = new MeasurementGrpcService(
                serviceMock.Object);

            var request = new RegisterMeasurementRequest
            {
                DetectorId = "DETECTOR-01",
                Timestamp =
                    Google.Protobuf.WellKnownTypes.Timestamp
                        .FromDateTimeOffset(timestamp),
                DoseRate = 12.5,
                Status = RadiationMonitor.API.Protos.DetectorStatus.Online
            };

            // Act
            var response = await service.RegisterMeasurement(
                request,
                null!);

            // Assert
            Assert.Equal(
                measurement.Id.ToString("D"),
                response.MeasurementId);
        }

        [Fact]
        public async Task RegisterMeasurement_WhenMeasurementIsInvalid_ReturnsInvalidArgument()
        {
            // Arrange
            var serviceMock =
                new Mock<IRegisterMeasurementService>();

            serviceMock
                .Setup(s => s.RegisterMeasurement(
                    It.IsAny<RegisterMeasurementCommand>()))
                .Throws(
                    new InvalidMeasurementException(
                        "DoseRate",
                        "Dose rate cannot be negative."));

            var service = new MeasurementGrpcService(
                serviceMock.Object);

            var request = new RegisterMeasurementRequest
            {
                DetectorId = "DETECTOR-01",
                Timestamp =
                    Google.Protobuf.WellKnownTypes.Timestamp
                        .FromDateTimeOffset(DateTimeOffset.UtcNow),
                DoseRate = -1.0,
                Status = RadiationMonitor.API.Protos.DetectorStatus.Online
            };

            // Act
            var exception = await Assert.ThrowsAsync<RpcException>(
                () => service.RegisterMeasurement(
                    request,
                    null!));

            // Assert
            Assert.Equal(
                StatusCode.InvalidArgument,
                exception.StatusCode);

            Assert.Equal(
                "Dose rate cannot be negative.",
                exception.Status.Detail);
        }

        [Fact]
        public async Task RegisterMeasurement_WhenDetectorStatusIsInvalid_ReturnsInvalidArgument()
        {
            // Arrange
            var expectedException =
                new ArgumentOutOfRangeException(
                    "status",
                    "Unknown detector status.");

            var serviceMock =
                new Mock<IRegisterMeasurementService>();

            serviceMock
                .Setup(s => s.RegisterMeasurement(
                    It.IsAny<RegisterMeasurementCommand>()))
                .Throws(expectedException);

            var service = new MeasurementGrpcService(
                serviceMock.Object);

            var request = new RegisterMeasurementRequest
            {
                DetectorId = "DETECTOR-01",
                Timestamp =
                    Google.Protobuf.WellKnownTypes.Timestamp
                        .FromDateTimeOffset(DateTimeOffset.UtcNow),
                DoseRate = 12.5,
                Status = RadiationMonitor.API.Protos.DetectorStatus.Online
            };

            // Act
            var exception = await Assert.ThrowsAsync<RpcException>(
                () => service.RegisterMeasurement(
                    request,
                    null!));

            // Assert
            Assert.Equal(
                StatusCode.InvalidArgument,
                exception.StatusCode);

            Assert.Equal(
                expectedException.Message,
                exception.Status.Detail);
        }

        [Fact]
        public async Task RegisterMeasurement_WithWarningStatus_MapsStatusCorrectly()
        {
            // Arrange
            var timestamp = DateTimeOffset.UtcNow;

            var measurement = new Measurement(
                "DETECTOR-01",
                timestamp,
                12.5,
                RadiationMonitor.Domain.Enums.DetectorStatus.Warning);

            var serviceMock =
                new Mock<IRegisterMeasurementService>();

            serviceMock
                .Setup(s => s.RegisterMeasurement(
                    It.Is<RegisterMeasurementCommand>(command =>
                        command.DetectorId == "DETECTOR-01" &&
                        command.Timestamp == timestamp &&
                        command.DoseRate == 12.5 &&
                        command.Status ==
                            RadiationMonitor.Domain.Enums.DetectorStatus.Warning)))
                .Returns(measurement);

            var service = new MeasurementGrpcService(
                serviceMock.Object);

            var request = new RegisterMeasurementRequest
            {
                DetectorId = "DETECTOR-01",
                Timestamp =
                    Google.Protobuf.WellKnownTypes.Timestamp
                        .FromDateTimeOffset(timestamp),
                DoseRate = 12.5,
                Status = RadiationMonitor.API.Protos.DetectorStatus.Warning
            };

            // Act
            var response = await service.RegisterMeasurement(
                request,
                null!);

            // Assert
            Assert.Equal(
                measurement.Id.ToString("D"),
                response.MeasurementId);
        }
    }
}
