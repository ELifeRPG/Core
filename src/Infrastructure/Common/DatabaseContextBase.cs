using System.Reflection;
using ELifeRPG.Application.Common;
using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Infrastructure.Common;

#pragma warning disable CS8618

public class DatabaseContextBase(string? connectionString)
    : DbContext, IDatabaseContext
{
    public DbSet<Account> Accounts { get; set; }

    public DbSet<Character> Characters { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Bank> Banks { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(
            connectionString,
            pgsql =>
            {
                pgsql.MigrationsAssembly(typeof(DatabaseContextBase).Assembly.GetName().Name);
            });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var types = typeof(Domain.Domain).Assembly.GetTypes();

        var typesDerivedFromIHasDomainEvents = types.Where(x => x.GetTypeInfo().ImplementedInterfaces.Any(type => type.GetTypeInfo().IsInterface && type == typeof(IHasDomainEvents)));
        foreach (var type in typesDerivedFromIHasDomainEvents)
        {
            modelBuilder.Entity(type).Ignore(nameof(IHasDomainEvents.DomainEvents));
        }

        var typesDerivedFromEntityBase = types.Where(x => x.GetTypeInfo().ImplementedInterfaces.Any(type => type.GetTypeInfo().IsClass && type == typeof(EntityBase)));
        foreach (var type in typesDerivedFromEntityBase)
        {
            modelBuilder.Entity(type).Property(nameof(EntityBase.Created)).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAdd();
        }
    }
}
