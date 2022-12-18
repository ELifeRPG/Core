using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment, Action<TracerProviderBuilder>? tracingBuilder = null)
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
                .AddMeter(Metrics.SourceName)
                .AddPrometheusExporter();
        });

        services.AddOpenTelemetryTracing(builder =>
        {
            builder.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(hostEnvironment.ApplicationName));
            builder.AddSource(Activities.SourceName);
            builder.AddEntityFrameworkCoreInstrumentation();
            builder.AddOtlpExporter(options => options.Endpoint = new Uri(configuration.GetConnectionString("OpenTelemetryTracingEndpoint")!));
            
            tracingBuilder?.Invoke(builder);
        });

        return services;
    }
    
    public static WebApplication MapInternalEndpoints(this WebApplication app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        
        return app;
    }
}
