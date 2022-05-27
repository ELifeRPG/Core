﻿using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Companies;
using Microsoft.EntityFrameworkCore;

namespace ELifeRPG.Infrastructure.Common;

public class DatabaseContextSeed
{
    private const long AccountSteam64Id = 76561198033445663;
    private static readonly Guid _characterJonDoeId = Guid.Parse("f408a4bd-cb83-4df5-a6a7-c4c3ddc5e4b2");
    private static readonly Guid _stateCompanyId = Guid.Parse("98a58b46-f9fd-4174-9d35-978fd3e5c41e");
    private static readonly Guid _statePolicyCompanyId = Guid.Parse("616c7e2c-c76d-4482-ae03-8e7afbb5ee39");
    
    public static async Task SeedSampleDataAsync(DatabaseContext context)
    {
        await SeedAccountsCharacters(context);
        await SeedCompanies(context);
    }

    private static async Task SeedAccountsCharacters(DatabaseContext context)
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

    private static async Task SeedCompanies(DatabaseContext context)
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
