using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment, Action<TracerProviderBuilder>? tracingBuilder = null)
    {
        services.AddDbContext<IReadDatabaseContext, ReadDatabaseContext>();
        services.AddDbContext<IReadWriteDatabaseContext, ReadWriteDatabaseContext>();

        services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddMeter(Metrics.SourceName)
                    .AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {
                tracing.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(hostEnvironment.ApplicationName));
                tracing.AddSource(Activities.SourceName);
                tracing.AddEntityFrameworkCoreInstrumentation();
                tracing.AddOtlpExporter(options => options.Endpoint = new Uri(configuration.GetConnectionString("Tracing")!));

                tracingBuilder?.Invoke(tracing);
            });

        return services;
    }

    public static WebApplication MapInternalEndpoints(this WebApplication app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();

        return app;
    }
}
