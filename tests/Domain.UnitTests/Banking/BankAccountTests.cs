using System;
using System.Collections.Generic;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common.Exceptions;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;
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
            Type = BankAccountType.Personal,
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
            OwningCompany = new Company("Feuerstein GmbH")
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

    [Fact]
    public void MakeTransactionTo_ThrowsELifeInvalidOperationException_WhenCheckingExecutingPersonOfPersonalBankAccount()
    {
        var bank = new Bank(new Country("DE"));
        
        var owningCharacter = new Character();
        var bankAccount = new BankAccount(bank, owningCharacter);
        
        var executingCharacter = new Character();
        var bankAccountOfExecutingCharacter = new BankAccount(bank, executingCharacter);

        Assert.Throws<ELifeInvalidOperationException>(() =>  bankAccount.MakeTransactionTo(bankAccountOfExecutingCharacter, executingCharacter, 1000m));
    }
    
    [Fact]
    public void MakeTransactionTo_AddsTransaction_WhenExecutingPersonOfPersonalBankAccountIsAuthorized()
    {
        var bank = new Bank(new Country("DE"));
        
        var owningCharacter = new Character();
        var bankAccount = new BankAccount(bank, owningCharacter);
        
        var bankAccountOfExecutingCharacter = new BankAccount(bank, new Character());

        bankAccount.MakeTransactionTo(bankAccountOfExecutingCharacter, owningCharacter, 1000m);

        Assert.Contains(bankAccount.Transactions, x => x.Amount == 1000m);
    }
    
    [Fact]
    public void MakeTransactionTo_ThrowsELifeInvalidOperationException_WhenCheckingExecutingPersonOfCompanyBankAccount()
    {
        var bank = new Bank(new Country("DE"));
        var owningCompany = new Company("Feuerstein GmbH");
        var bankAccount = new BankAccount(bank, owningCompany);

        var bankAccountOfExecutingCharacter = new BankAccount(bank, new Company("The Empire"));

        Assert.Throws<ELifeInvalidOperationException>(() =>  bankAccount.MakeTransactionTo(bankAccountOfExecutingCharacter, new Character(), 1000m));
    }
}
