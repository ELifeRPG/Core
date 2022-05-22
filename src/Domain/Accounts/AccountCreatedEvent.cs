using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Accounts;

public class AccountCreatedEvent : DomainEvent
{
    public AccountCreatedEvent(Account account)
    {
        Account = account;
    }
    
    public Account Account { get; }
}
