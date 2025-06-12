using ELifeRPG.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ELifeRPG.Infrastructure.Common;

public sealed class ReadDatabaseContext(IConfiguration configuration)
    : DatabaseContextBase(configuration.GetConnectionString("database+read")), IReadDatabaseContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}
