using ELifeRPG.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ELifeRPG.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly))
            .AddSingleton(typeof(IPipelineBehavior<,>), typeof(MetricsBehaviour<,>));
    }
}
