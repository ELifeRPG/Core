using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Application.Common;

#pragma warning disable CS8618

public interface IDatabaseContext : IDisposable, IAsyncDisposable
{
    DbSet<Account> Accounts { get; set; }

    DbSet<Character> Characters { get; set; }

    DbSet<Company> Companies { get; set; }

    DbSet<Country> Countries { get; set; }

    DbSet<Bank> Banks { get; set; }

    DbSet<BankAccount> BankAccounts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
