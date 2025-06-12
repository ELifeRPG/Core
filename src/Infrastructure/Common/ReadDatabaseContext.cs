using ELifeRPG.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ELifeRPG.Infrastructure.Common;

public class ReadDatabaseContext(DbContextOptions<DatabaseContextBase> options, IConfiguration configuration) : DatabaseContextBase(options, configuration.GetConnectionString("database+read")), IReadDatabaseContext;
