using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Domain.Enums;

/// <summary>
/// Status : Online, Warning or Error
/// </summary>
public enum DetectorStatus { 
    Online, 
    Warning, 
    Error
};
