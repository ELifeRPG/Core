using ELifeRPG.Application.Common.Behaviours;
using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace ELifeRPG.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediator(options =>
            {
                options.Assemblies = [typeof(ApplicationExtensions)];
                options.ServiceLifetime = ServiceLifetime.Transient;
            })
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(MetricsBehaviour<,>));
    }
}
