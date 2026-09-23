using RadiationMonitor.Domain.Enums;

namespace RadiationMonitor.API.Models
{
    public class RegisterMeasurementRequest
    {
        public string DetectorId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public double DoseRate { get; set; }
        public DetectorStatus Status { get; set; }
    }
}
