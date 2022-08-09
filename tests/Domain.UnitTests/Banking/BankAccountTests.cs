using System;
using System.Collections.Generic;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Companies;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Banking;

public class BankAccountTests
{
    [Fact]
    public void Can_ChecksCapabilities_WhenAccountIsOfOwningCharacter()
    {
        var character = new Character { Id = Guid.Parse("BC9B0D5B-F1CB-443D-98DA-CB5451AA09C0") };
        
        var bankAccount = new BankAccount
        {
            OwningCharacter = character,
        };

        var canManageFinances = bankAccount.Can(character, BankAccountCapabilities.CommitTransactions);

        Assert.True(canManageFinances);
    }
    
    [Fact]
    public void Can_ChecksCapabilities_WhenAccountIsOfOwningCompany()
    {
        var character = new Character { Id = Guid.Parse("BC9B0D5B-F1CB-443D-98DA-CB5451AA09C0") };
        
        var bankAccount = new BankAccount
        {
            OwningCompany = new Company
            {
                Memberships = new List<CompanyMembership>
                {
                    new()
                    {
                        Character = character,
                        Position = new CompanyPosition
                        {
                            Permissions = CompanyPermissions.ManageFinances,
                        },
                    },
                },
            },
        };

        var canManageFinances = bankAccount.Can(character, BankAccountCapabilities.CommitTransactions);

        Assert.True(canManageFinances);
    }
}
