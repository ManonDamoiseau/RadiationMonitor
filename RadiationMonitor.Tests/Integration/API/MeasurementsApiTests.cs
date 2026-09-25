using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using RadiationMonitor.Domain.Enums;
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
            await using var factory = new WebApplicationFactory<Program>();

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
            await using var factory = new WebApplicationFactory<Program>();

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

    }
}
