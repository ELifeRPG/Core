using System.Linq;
using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Common.Exceptions;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Accounts;

public class AccountTests
{
    [Fact]
    public void CreatingAccount_ShouldRaiseAccountCreatedEvent()
    {
        var account = new Account(long.MinValue);

        Assert.Equal(1, account.DomainEvents.Count(x => x.GetType() == typeof(AccountCreatedEvent)));
    }

    [Fact]
    public void Lock_SetsStatusToLocked()
    {
        var account = new Account { Status = AccountStatus.Active };

        account.Lock();

        Assert.Equal(AccountStatus.Locked, account.Status);
    }

    [Fact]
    public void Lock_RaisesAccountLockedEvent()
    {
        var account = new Account { Status = AccountStatus.Active };

        account.Lock();

        Assert.Equal(1, account.DomainEvents.Count(x => x.GetType() == typeof(AccountLockedEvent)));
    }

    [Fact]
    public void Lock_ThrowsELifeInvalidOperationException_WhenStatusIsLocked()
    {
        var account = new Account { Status = AccountStatus.Locked };

        var exception = Assert.Throws<ELifeInvalidOperationException>(() => account.Lock());
        Assert.Equal("Account is already locked.", exception.Message);
    }

    [Fact]
    public void Unlock_SetsStatusToActive()
    {
        var account = new Account { Status = AccountStatus.Locked };

        account.Unlock();

        Assert.Equal(AccountStatus.Active, account.Status);
    }

    [Fact]
    public void Unlock_RaisesAccountUnlockedEvent()
    {
        var account = new Account { Status = AccountStatus.Locked };

        account.Unlock();

        Assert.Equal(1, account.DomainEvents.Count(x => x.GetType() == typeof(AccountUnlockedEvent)));
    }

    [Fact]
    public void Unlock_ThrowsELifeInvalidOperationException_WhenStatusIsActive()
    {
        var account = new Account { Status = AccountStatus.Active };

        var exception = Assert.Throws<ELifeInvalidOperationException>(() => account.Unlock());
        Assert.Equal("Account is already active.", exception.Message);
    }
}
