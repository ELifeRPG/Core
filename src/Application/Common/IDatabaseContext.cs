using ELifeRPG.Domain.Characters;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Common;

public interface IDatabaseContext
{
    DbSet<Character> Characters { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}
