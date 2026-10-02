using Grpc.Core;
using RadiationMonitor.API.Protos;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;

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
            throw new NotImplementedException();
        }
    }
}
