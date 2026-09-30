using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using RadiationMonitor.API.Controllers;
using RadiationMonitor.API.Models;
using RadiationMonitor.Application.Measurements.GetMeasurement;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Tests.Unit.API
{
    public class MeasurementsControllerTests
    {
        [Fact]
        public void RegisterMeasurement_ReturnsCreatedWithMeasurementResponse()
        {
            // Arrange
            var measurement = new Measurement(
                "DETECTOR-01",
                DateTimeOffset.UtcNow,
                12.5,
                DetectorStatus.Online);

            var serviceMock = new Mock<IRegisterMeasurementService>();
            
            serviceMock
                .Setup(s => s.RegisterMeasurement(
                    It.IsAny<RegisterMeasurementCommand>()))
                .Returns(measurement);

            var getMeasurementServiceMock =
                new Mock<IGetMeasurementService>();

            var controller = new MeasurementsController(
                serviceMock.Object,
                getMeasurementServiceMock.Object);

            var request = new RegisterMeasurementRequest
            {
                DetectorId = "DETECTOR-01",
                Timestamp = DateTimeOffset.UtcNow,
                DoseRate = 12.5,
                Status = DetectorStatus.Online
            };

            // Act
            var result = controller.RegisterMeasurement(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(
                StatusCodes.Status201Created,
                objectResult.StatusCode);

            var returnedMeasurement =
                Assert.IsType<MeasurementResponse>(objectResult.Value);

            Assert.Equal(
            measurement.Id,
            returnedMeasurement.Id);

            Assert.Equal(
                measurement.DetectorId,
                returnedMeasurement.DetectorId);

            Assert.Equal(
                measurement.Timestamp,
                returnedMeasurement.Timestamp);

            Assert.Equal(
                measurement.DoseRate,
                returnedMeasurement.DoseRate);

            Assert.Equal(
                measurement.Status,
                returnedMeasurement.Status);

            serviceMock.Verify(
                s => s.RegisterMeasurement(
                    It.IsAny<RegisterMeasurementCommand>()),
                Times.Once);
        }
        
    }
}
