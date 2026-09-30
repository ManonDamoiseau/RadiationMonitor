using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using RadiationMonitor.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadiationMonitor.Tests.Integration.API
{
    public class RadiationMonitorWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<
                    DbContextOptions<RadiationMonitorDbContext>>();

                services.AddDbContext<RadiationMonitorDbContext>(options =>
                {
                    options.UseSqlServer(
                        "Server=(localdb)\\MSSQLLocalDB;" +
                        "Database=RadiationMonitor_Test;" +
                        "Trusted_Connection=True;" +
                        "TrustServerCertificate=True");
                });
            });
        }

        protected override IHost CreateHost(
            IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            using var scope = host.Services.CreateScope();

            var dbContext =
                scope.ServiceProvider
                    .GetRequiredService<RadiationMonitorDbContext>();

            dbContext.Database.Migrate();

            return host;
        }
    }
}
