using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Characters;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Common;

public interface IDatabaseContext
{
    DbSet<Account> Accounts { get; set; }
    
    DbSet<Character> Characters { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}
