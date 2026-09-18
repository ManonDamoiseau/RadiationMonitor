using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadiationMonitor.Infrastructure.Persistence;

namespace RadiationMonitor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RadiationMonitor");

            services.AddDbContext<RadiationMonitorDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}
