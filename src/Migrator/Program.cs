using ELifeRPG.Application;
using ELifeRPG.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddApplication();
        services.AddInfrastructure(context.Configuration, context.HostingEnvironment);
    })
    .Build();

using var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ReadWriteDatabaseContext>();

await context.Database.MigrateAsync();
await DatabaseContextSeed.SeedSampleDataAsync(context);
