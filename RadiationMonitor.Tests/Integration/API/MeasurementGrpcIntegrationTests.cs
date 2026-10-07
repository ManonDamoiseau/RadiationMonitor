using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using RadiationMonitor.API.Protos;
using RadiationMonitor.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace RadiationMonitor.Tests.Integration.API
{
    [Collection("IntegrationTests")]
    public class MeasurementGrpcIntegrationTests
    {
        [Fact]
        public async Task RegisterMeasurement_WithValidRequest_ReturnsMeasurementId()
        {
            // Arrange
            await using var factory =
                new RadiationMonitorWebApplicationFactory();

            using var httpClient =
                factory.CreateDefaultClient();

            using var channel =
                GrpcChannel.ForAddress(
                    httpClient.BaseAddress!,
                    new GrpcChannelOptions
                    {
                        HttpClient = httpClient
                    });

            var client =
                new MeasurementService.MeasurementServiceClient(
                    channel);

            var timestamp = DateTimeOffset.UtcNow;

            var request = new RegisterMeasurementRequest
            {
                DetectorId = "DETECTOR-01",
                Timestamp =
                    Timestamp.FromDateTimeOffset(timestamp),
                DoseRate = 12.5,
                Status = DetectorStatus.Online
            };

            // Act
            var response =
                await client.RegisterMeasurementAsync(request);

            // Assert
            Assert.NotEqual(
                Guid.Empty.ToString("D"),
                response.MeasurementId);

            Assert.True(
                Guid.TryParse(
                    response.MeasurementId,
                    out var measurementId));

            Assert.NotEqual(
                Guid.Empty,
                measurementId);

            await using var scope =
                factory.Services.CreateAsyncScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<RadiationMonitorDbContext>();

            var persistedMeasurement =
                await dbContext.Measurements
                    .SingleOrDefaultAsync(
                        m => m.Id == measurementId);

            Assert.NotNull(persistedMeasurement);

            Assert.Equal(
                "DETECTOR-01",
                persistedMeasurement!.DetectorId);

            Assert.Equal(
                timestamp,
                persistedMeasurement.Timestamp);

            Assert.Equal(
                12.5,
                persistedMeasurement.DoseRate);

            Assert.Equal(
                RadiationMonitor.Domain.Enums.DetectorStatus.Online,
                persistedMeasurement.Status);

        }
    }
}
