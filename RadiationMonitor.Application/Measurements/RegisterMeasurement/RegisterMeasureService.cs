using RadiationMonitor.Domain.Enums;
using RadiationMonitor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

// Comments in French for my own reference on this page, to make things clearer whilst I’m setting it up

namespace RadiationMonitor.Application.Measurements.RegisterMeasurement
{
    public class RegisterMeasurementService
    {
        private readonly IMeasurementRepository _measurementRepository; // je crée un objet de type IMeasurementRepository non modifiable nommée _measurementRepository

        public RegisterMeasurementService(IMeasurementRepository measurementRepository) // constructeur d'un objet de la classe RegisterMeasurementService, on copie sa référence dans le champ privé
        {
            _measurementRepository = measurementRepository; //_measurementRepository n'est pas le Repository mais une référence vers un objet Repository --> polymorphisme
        }

        public Measurement RegisterMeasurement(RegisterMeasurementCommand command) // Méthode qui récupère la variable command de type RegisterMeasurementCommand command
        {
            Measurement measurement = new Measurement(
            command.DetectorId,
            command.Timestamp,
            command.DoseRate,
            command.Status
            ); // Création d'un nouvel objet de la classe Measurement, nommé measurement, avec les infos transmises dans command.

            _measurementRepository.Add(measurement); // on applique la méthode Add de l'interface IMeasurementRepository à l'objet _measurementRepository, avec le paramètre measurement, donc on enregistre les données de measurement via _measurementRepository
            
            return measurement;// RegisterMeasurement retourne measurement

        }

        

    }
}
