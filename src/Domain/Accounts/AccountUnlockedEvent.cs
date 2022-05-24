using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Accounts;

public class AccountUnlockedEvent : DomainEvent
{
    public AccountUnlockedEvent(Account account)
    {
        Account = account;
    }
    
    public Account Account { get; }
}
