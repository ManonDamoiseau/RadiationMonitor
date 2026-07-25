using RadiationMonitor.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Domain.Entities
{
    internal class Measurement // Les mesures envoyées par le détecteur
    {
        public Guid  MeasurementId { get; set; } // Global Unique Identifier, identifiant unique
        public string DetectorId { get; set; }
        public DateTimeOffset Timestamp { get; set; } // DateTimeOffset tiens compte du fuseau horaire
        public double DoseRate { get; set; } //double est plus précis que float, plus pertinent pour mesures physiques
        public double Temperature { get; set; }
        public DetectorStatus Status {  get; set; }

    }
}
