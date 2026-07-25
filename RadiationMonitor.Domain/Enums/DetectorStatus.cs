using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Domain.Enums
{
    internal class DetectorStatus // Les différents status du détecteur
    {
        public enum DetectorStatusEnum { //enum car un seul status possible à la fois
            Online, 
            Warning, 
            Error
        };
    }
}
