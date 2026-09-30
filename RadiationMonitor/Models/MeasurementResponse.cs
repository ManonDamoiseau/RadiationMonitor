using RadiationMonitor.Domain.Enums;

namespace RadiationMonitor.API.Models
{
    public class MeasurementResponse
    {
        public Guid Id { get; init; }
        public string DetectorId { get; init; } = null!;
        public DateTimeOffset Timestamp { get; init; }
        public double DoseRate { get; init; }
        public DetectorStatus Status { get; init; }
    }
}
