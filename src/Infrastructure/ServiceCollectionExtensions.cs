using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Metrics;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Database"),
                pgsql =>
                {
                    pgsql.MigrationsAssembly(typeof(DatabaseContext).Assembly.GetName().Name);
                }));

        services.AddOpenTelemetryMetrics(builder =>
        {
            builder
                .AddMeter("ELifeRPG")
                .AddPrometheusExporter();
        });

        return services;
    }
    
    public static WebApplication MapInternalEndpoints(this WebApplication app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        
        return app;
    }
}
