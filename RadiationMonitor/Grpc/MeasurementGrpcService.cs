using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RadiationMonitor.API.Protos;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Domain.Exceptions;


namespace RadiationMonitor.API.Grpc
 
{
    public class MeasurementGrpcService : MeasurementService.MeasurementServiceBase
    {
        private readonly IRegisterMeasurementService _registerMeasurementService;
        public MeasurementGrpcService(
            IRegisterMeasurementService registerMeasurementService)
        {
            _registerMeasurementService = registerMeasurementService;
        }
        public override Task<RegisterMeasurementResponse> RegisterMeasurement(
            RegisterMeasurementRequest request,
            ServerCallContext context)
        {
            try
            {
                var command = new RegisterMeasurementCommand(
                    request.DetectorId,
                    request.Timestamp.ToDateTimeOffset(),
                    request.DoseRate,
                    MapDetectorStatus(request.Status));

                var measurement =
                    _registerMeasurementService.RegisterMeasurement(command);

                return Task.FromResult(new RegisterMeasurementResponse
                {
                    MeasurementId = measurement.Id.ToString("D")
                });
            }
            catch (InvalidMeasurementException ex)
            {
                throw new RpcException(
                    new Status(
                        StatusCode.InvalidArgument,
                        ex.Message));
            }
        }

        private static Domain.Enums.DetectorStatus MapDetectorStatus(Protos.DetectorStatus status)
        {
            return status switch
            {
                Protos.DetectorStatus.Unknown => Domain.Enums.DetectorStatus.Unknown,
                Protos.DetectorStatus.Online => Domain.Enums.DetectorStatus.Online,
                Protos.DetectorStatus.Warning => Domain.Enums.DetectorStatus.Warning,
                Protos.DetectorStatus.Error => Domain.Enums.DetectorStatus.Error,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unknown detector status.")
            };
        }

    }
}
