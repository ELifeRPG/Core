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
    public void TransferMoneyTo_ThrowsELifeInvalidOperationException_WhenCheckingExecutingPersonOfPersonalBankAccount()
    {
        var bank = new Bank(new Country("DE"));
        
        var owningCharacter = new Character();
        var bankAccount = new BankAccount(bank, owningCharacter){ Bookings = new List<BankAccountBooking>() };
        
        var executingCharacter = new Character();
        var bankAccountOfExecutingCharacter = new BankAccount(bank, executingCharacter){ Bookings = new List<BankAccountBooking>() };

        Assert.Throws<ELifeInvalidOperationException>(() =>  bankAccount.TransferMoneyTo(bankAccountOfExecutingCharacter, executingCharacter, 1000m));
    }
    
    [Fact]
    public void TransferMoneyTo_AddsBooking_WhenExecutingPersonOfPersonalBankAccountIsAuthorized()
    {
        var bank = new Bank(new Country("DE"));
        
        var owningCharacter = new Character();
        var bankAccount = new BankAccount(bank, owningCharacter){ Balance = 5000m, Bookings = new List<BankAccountBooking>() };
        
        var bankAccountOfExecutingCharacter = new BankAccount(bank, new Character()){ Bookings = new List<BankAccountBooking>() };

        bankAccount.TransferMoneyTo(bankAccountOfExecutingCharacter, owningCharacter, 1000m);

        Assert.Equal(1, bankAccount.Bookings.Count);
    }
    
    [Fact]
    public void TransferMoneyTo_ThrowsELifeInvalidOperationException_WhenCheckingExecutingPersonOfCompanyBankAccount()
    {
        var bank = new Bank(new Country("DE"));
        var owningCompany = new Company("Feuerstein GmbH");
        var bankAccount = new BankAccount(bank, owningCompany){ Bookings = new List<BankAccountBooking>() };

        var bankAccountOfExecutingCharacter = new BankAccount(bank, new Company("The Empire")){ Bookings = new List<BankAccountBooking>() };

        Assert.Throws<ELifeInvalidOperationException>(() =>  bankAccount.TransferMoneyTo(bankAccountOfExecutingCharacter, new Character(), 1000m));
    }

    [Fact]
    public void WithdrawMoney_AddsBooking()
    {
        var bank = new Bank(new Country("EL"));
        var owningCharacter = new Character();
        var bankAccount = new BankAccount(bank, owningCharacter){ Balance = 1000m, Bookings = new List<BankAccountBooking>() };

        bankAccount.WithdrawMoney(owningCharacter, 200);

        Assert.Contains(bankAccount.Bookings, x => x.Type == BankAccountBookingType.Outgoing);
    }
    
    [Fact]
    public void TransferMoneyTo_AddsBookingsToBothAccounts()
    {
        var bank = new Bank(new Country("EL"));
        var character = new Character();
        var bankAccount = new BankAccount(bank, character){ Balance = 1000m, Bookings = new List<BankAccountBooking>() };
        var targetBankAccount = new BankAccount(bank, new Character()){ Bookings = new List<BankAccountBooking>() };

        bankAccount.TransferMoneyTo(targetBankAccount, character, 200);

        Assert.Contains(bankAccount.Bookings, x => x.Type == BankAccountBookingType.Outgoing);
        Assert.Contains(targetBankAccount.Bookings, x => x.Type == BankAccountBookingType.Incoming);
    }
}
