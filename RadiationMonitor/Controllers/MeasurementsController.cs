using Microsoft.AspNetCore.Mvc;
using RadiationMonitor.API.Models;
using RadiationMonitor.Application.Measurements.GetMeasurement;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
namespace RadiationMonitor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IRegisterMeasurementService _registerMeasurementService;
        private readonly IGetMeasurementService _getMeasurementService;

        public MeasurementsController(
           IRegisterMeasurementService registerMeasurementService,
           IGetMeasurementService getMeasurementService)
        {
            _registerMeasurementService = registerMeasurementService;
            _getMeasurementService = getMeasurementService;
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

            var response = new MeasurementResponse
            {
                Id = measurement.Id,
                DetectorId = measurement.DetectorId,
                Timestamp = measurement.Timestamp,
                DoseRate = measurement.DoseRate,
                Status = measurement.Status
            };

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetMeasurement(Guid id)
        {
            var query = new GetMeasurementQuery(id);

            var measurement =
                _getMeasurementService.GetMeasurement(query);

            if (measurement is null)
            {
                return NotFound();
            }

            var response = new MeasurementResponse
            {
                Id = measurement.Id,
                DetectorId = measurement.DetectorId,
                Timestamp = measurement.Timestamp,
                DoseRate = measurement.DoseRate,
                Status = measurement.Status
            };

            return Ok(response);
        }

    }

}
