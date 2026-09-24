using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Domain.Exceptions
{
    public class InvalidMeasurementException : Exception
    {
        public string PropertyName { get; }

        public InvalidMeasurementException(
            string propertyName,
            string message)
            : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
