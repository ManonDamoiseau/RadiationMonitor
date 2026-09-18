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

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IMeasurementRepository, InMemoryMeasurementRepository>();
builder.Services.AddScoped<RegisterMeasurementService>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
