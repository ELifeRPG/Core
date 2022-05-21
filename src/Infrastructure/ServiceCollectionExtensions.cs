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
            options.UseSqlite("Data Source=E:\\Projekte\\private\\ELifeRPG\\Core\\temp.sqlite", x => x.MigrationsAssembly(typeof(DatabaseContext).Assembly.GetName().Name)));

        services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        return services;
    }
}
