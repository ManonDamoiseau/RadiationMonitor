using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RadiationMonitor.Domain.Exceptions;

namespace RadiationMonitor.API.Exceptions;

public class InvalidMeasurementExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext,
    Exception exception,
    CancellationToken cancellationToken)
    {
        if (exception is not InvalidMeasurementException invalidMeasurementException)
        {
            return false;
        }

        var problemDetails = new ProblemDetails
        {
            Title = "Invalid measurement",
            Status = StatusCodes.Status400BadRequest,
            Detail = invalidMeasurementException.Message
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}

