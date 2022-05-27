﻿using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("Database"),
                pgsql =>
                {
                    pgsql.MigrationsAssembly(typeof(DatabaseContext).Assembly.GetName().Name);
                }));

        services.AddScoped<IDatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        return services;
    }
}
