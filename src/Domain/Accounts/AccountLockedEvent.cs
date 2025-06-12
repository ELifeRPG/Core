using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Accounts;

public class AccountLockedEvent : DomainEvent
{
    public AccountLockedEvent(Account account)
    {
        Account = account;
    }

    public Account Account { get; }
}
