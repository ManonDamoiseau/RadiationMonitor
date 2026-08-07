using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Domain.Enums;

/// <summary>
/// Status : Unknown, Online, Warning or Error
/// </summary>
public enum DetectorStatus { 
    Unknown,
    Online, 
    Warning, 
    Error
};
