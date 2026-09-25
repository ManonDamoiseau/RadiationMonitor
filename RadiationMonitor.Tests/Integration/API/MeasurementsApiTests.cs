using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Entities;
using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Infrastructure.Persistence;
using System.Net;
using System.Net.Http.Json;

namespace RadiationMonitor.Tests.Integration.API
{
    public class MeasurementsApiTests
    {
        public static IEnumerable<object[]> InvalidMeasurements =>
            new List<object[]>
            {
                new object[]
                {
                    "   ",
                    DateTimeOffset.UtcNow,
                    12.5,
                    DetectorStatus.Online,
                    "Detector ID cannot be empty or whitespace."
                },
                new object[]
                {
                    "DETECTOR-01",
                    default(DateTimeOffset),
                    12.5,
                    DetectorStatus.Online,
                    "Timestamp cannot be the default value."
                },
                new object[]
                {
                    "DETECTOR-01",
                    DateTimeOffset.UtcNow,
                    -1.0,
                    DetectorStatus.Online,
                    "Dose rate cannot be negative."
                },
                new object[]
                {
                    "DETECTOR-01",
                    DateTimeOffset.UtcNow,
                    12.5,
                    DetectorStatus.Unknown,
                    "Unknown status is not allowed."
                },
                new object[]
                {
                    "DETECTOR-01",
                    DateTimeOffset.UtcNow,
                    12.5,
                    DetectorStatus.Error,
                    "Error status is not allowed."
                }
            };

        [Theory]
        [MemberData(nameof(InvalidMeasurements))]
        public async Task RegisterMeasurement_InvalidMeasurement_ReturnsBadRequest(
            string? detectorId,
            DateTimeOffset timestamp,
            double doseRate,
            DetectorStatus status,
            string expectedDetail)
        {
            await using var factory =
                new RadiationMonitorWebApplicationFactory();

            using var client = factory.CreateClient();

            var request = new
            {
                DetectorId = detectorId,
                Timestamp = timestamp,
                DoseRate = doseRate,
                Status = status
            };

            var response = await client.PostAsJsonAsync(
                "/api/Measurements",
                request);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);

            var problemDetails =
                await response.Content.ReadFromJsonAsync<ProblemDetails>();

            Assert.NotNull(problemDetails);

            Assert.Equal(
                "Invalid measurement",
                problemDetails!.Title);

            Assert.Equal(
                StatusCodes.Status400BadRequest,
                problemDetails.Status);

            Assert.Equal(
                expectedDetail,
                problemDetails.Detail);
        }

        [Fact]
        public async Task RegisterMeasurement_WithNullDetectorId_ReturnsBadRequest()
        {
            await using var factory =
                new RadiationMonitorWebApplicationFactory();

            using var client = factory.CreateClient();

            var request = new
            {
                DetectorId = (string?)null,
                Timestamp = DateTimeOffset.UtcNow,
                DoseRate = 12.5,
                Status = DetectorStatus.Online
            };

            var response = await client.PostAsJsonAsync(
                "/api/Measurements",
                request);

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode);
        }
        [Fact]
        public async Task RegisterMeasurement_WithValidRequest_ReturnsCreated()
        {
            // Arrange
            await using var factory =
                new RadiationMonitorWebApplicationFactory();

            using var client = factory.CreateClient();

            var request = new
            {
                DetectorId = "DETECTOR-01",
                Timestamp = DateTimeOffset.UtcNow,
                DoseRate = 12.5,
                Status = DetectorStatus.Online
            };

            // Act
            var response = await client.PostAsJsonAsync(
                "/api/Measurements",
                request);

            // Assert - HTTP response
            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);

            var measurement =
                await response.Content
                    .ReadFromJsonAsync<MeasurementResponse>();

            Assert.NotNull(measurement);


            Assert.Equal(
                request.DetectorId,
                measurement!.DetectorId);

            Assert.Equal(
                request.Timestamp,
                measurement.Timestamp);

            Assert.Equal(
                request.DoseRate,
                measurement.DoseRate);

            Assert.Equal(
                request.Status,
                measurement.Status);

            Assert.NotEqual(
                Guid.Empty,
                measurement.Id);

            // Assert - database persistence
            await using var scope =
                factory.Services.CreateAsyncScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<RadiationMonitorDbContext>();

            var persistedMeasurement =
                await dbContext.Measurements
                   .SingleOrDefaultAsync(
                    m => m.Id == measurement.Id);

            Assert.NotNull(persistedMeasurement);

            Assert.Equal(
                measurement.Id,
                persistedMeasurement!.Id);

            Assert.Equal(
                request.DetectorId,
                persistedMeasurement.DetectorId);

            Assert.Equal(
                request.Timestamp,
                persistedMeasurement.Timestamp);

            Assert.Equal(
                request.DoseRate,
                persistedMeasurement.DoseRate);

            Assert.Equal(
                request.Status,
                persistedMeasurement.Status);

        }
        private sealed class MeasurementResponse
        {
            public Guid Id { get; set; }
            public string DetectorId { get; set; } = null!;
            public DateTimeOffset Timestamp { get; set; }
            public double DoseRate { get; set; }
            public DetectorStatus Status { get; set; }
        }
    }
}
