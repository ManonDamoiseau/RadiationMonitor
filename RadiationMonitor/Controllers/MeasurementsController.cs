using Microsoft.AspNetCore.Mvc;
using RadiationMonitor.API.Models;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
namespace RadiationMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IRegisterMeasurementService _registerMeasurementService;

        public MeasurementsController(
            IRegisterMeasurementService registerMeasurementService)
        {
            _registerMeasurementService = registerMeasurementService;
        }

        [HttpPost]
        public IActionResult RegisterMeasurement(
            RegisterMeasurementRequest request)
        {
            var command = new RegisterMeasurementCommand(
                request.DetectorId,
                request.Timestamp,
                request.DoseRate,
                request.Status);

            var measurement =
                _registerMeasurementService.RegisterMeasurement(command);

            return StatusCode(StatusCodes.Status201Created, measurement);
        }
    }

}
