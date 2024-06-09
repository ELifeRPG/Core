using ELifeRPG.Application.Accounts;
using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Accounts;
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
        services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Database"),
                pgsql =>
                {
                    pgsql.MigrationsAssembly(typeof(DatabaseContext).Assembly.GetName().Name);
                }));

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
                tracing.AddOtlpExporter(options => options.Endpoint = new Uri(configuration.GetConnectionString("OpenTelemetryTracingEndpoint")!));

                tracingBuilder?.Invoke(tracing);
            });

        services.AddSingleton<IVerificationTokenValidator, VerificationTokenValidator>();

        return services;
    }

    public static WebApplication MapInternalEndpoints(this WebApplication app)
    {
        app.UseOpenTelemetryPrometheusScrapingEndpoint();
        
        return app;
    }
}
