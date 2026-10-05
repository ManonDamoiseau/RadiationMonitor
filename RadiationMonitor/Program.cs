using RadiationMonitor.API.Exceptions;
using RadiationMonitor.API.Grpc;
using RadiationMonitor.Application.Measurements.GetMeasurement;
using RadiationMonitor.Application.Measurements.RegisterMeasurement;
using RadiationMonitor.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider((context, options) =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddGrpc();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<
    IRegisterMeasurementService,
    RegisterMeasurementService>();

builder.Services.AddScoped<
    IGetMeasurementService,
    GetMeasurementService>();

builder.Services.AddExceptionHandler<InvalidMeasurementExceptionHandler>();

builder.Services.AddProblemDetails();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<MeasurementGrpcService>();

app.Run();

public partial class Program { }

