using Grpc.Core;
using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using RadiationMonitor.API.Protos;

var apiAddress = "https://localhost:7105";

using var channel = GrpcChannel.ForAddress(apiAddress);

var client = new MeasurementService.MeasurementServiceClient(channel);

var request = new RegisterMeasurementRequest
{
    DetectorId = "SIMULATOR-001",
    Timestamp = Timestamp.FromDateTimeOffset(DateTimeOffset.UtcNow),
    DoseRate = 0.12,
    Status = DetectorStatus.Online
};

try
{
    var response = await client.RegisterMeasurementAsync(request);

    Console.WriteLine(
        $"Measurement registered successfully. ID: {response.MeasurementId}");
}
catch (RpcException ex)
{
    Console.Error.WriteLine(
        $"gRPC error: {ex.StatusCode} - {ex.Status.Detail}");
    Environment.ExitCode = 1;
}
