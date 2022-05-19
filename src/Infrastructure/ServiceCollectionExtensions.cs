using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite("Data Source=temp.sqlite"));

        services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        return services;
    }
}
