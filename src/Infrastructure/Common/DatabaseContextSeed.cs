using ELifeRPG.Application.Common;
using ELifeRPG.Application.Common.Exceptions;
using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Infrastructure.Common;

public class DatabaseContextSeed
{
    private const long AccountSteam64Id = 76561198033445663;
    private static readonly Guid _stateBankId = Guid.Parse("48654229-7f6e-4961-bc2e-52247d10ff22");
    private static readonly Guid _characterJonDoeId = Guid.Parse("f408a4bd-cb83-4df5-a6a7-c4c3ddc5e4b2");
    private static readonly Guid _stateCompanyId = new CompanyId(Guid.Parse("98a58b46-f9fd-4174-9d35-978fd3e5c41e")).Value;
    private static readonly Guid _statePolicyCompanyId = new CompanyId(Guid.Parse("616c7e2c-c76d-4482-ae03-8e7afbb5ee39")).Value;
    
    public static async Task SeedSampleDataAsync(IDatabaseContext context)
    {
        await SeedCountries(context);
        await SeedBanking(context);
        await SeedAccountsCharacters(context);
        await SeedCompanies(context);
    }

    private static async Task SeedCountries(IDatabaseContext context)
    {
        var country = await context.Countries.SingleOrDefaultAsync(x => x.Id == Country.Default.Id);
        if (country is null)
        {
            context.Countries.Add(Country.Default);
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task SeedBanking(IDatabaseContext context)
    {
        var stateBank = await context.Banks.SingleOrDefaultAsync(x => x.Id == _stateBankId);
        if (stateBank is null)
        {
            var country = await context.Countries
                .Include(x => x.Banks)
                .SingleOrDefaultAsync(x => x.Id == Country.Default.Id);
            
            if (country is null)
            {
                throw new ELifeEntityNotFoundException();
            }
            
            context.Banks.Add(new Bank(country));
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAccountsCharacters(IDatabaseContext context)
    {
        if ((await context.Characters.SingleOrDefaultAsync(x => x.Id == _characterJonDoeId)) is null)
        {
            context.Accounts.Add(new Account
            {
                Id = Guid.NewGuid(),
                SteamId = AccountSteam64Id,
                Characters = new List<Character>
                {
                    new()
                    {
                        Id = _characterJonDoeId,
                        Name = new CharacterName("Jon", "Doe"),
                    },
                },
            });
            
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCompanies(IDatabaseContext context)
    {
        if ((await context.Companies.SingleOrDefaultAsync(x => x.Id == _stateCompanyId)) is null)
        {
            var companyPosition = new CompanyPosition
            {
                Name = "Head of State", 
                Ordering = 100,
            };
            
            var company = new Company
            {
                Id = _stateCompanyId,
                Name = "State",
                Positions = new List<CompanyPosition> { companyPosition },
                Memberships = new List<CompanyMembership>(),
            };
            
            company.AddMembership(await context.Characters.FirstAsync(), companyPosition);

            context.Companies.Add(company);
            await context.SaveChangesAsync();
        }
        
        if ((await context.Companies.SingleOrDefaultAsync(x => x.Id == _statePolicyCompanyId)) is null)
        {
            var companyPosition = new CompanyPosition
            {
                Name = "Head of State Police", 
                Ordering = 100,
            };
            
            var company = new Company
            {
                Id = _statePolicyCompanyId,
                Name = "State Police",
                Positions = new List<CompanyPosition> { companyPosition },
                Memberships = new List<CompanyMembership>(),
            };
            
            company.AddMembership(await context.Characters.FirstAsync(), companyPosition);

            context.Companies.Add(company);
            await context.SaveChangesAsync();
        }
    }
}
