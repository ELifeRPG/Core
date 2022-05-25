using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Accounts;

public class Account : EntityBase, IHasDomainEvents
{
    public Guid Id { get; init; }

    public long SteamId { get; init; }

    public AccountStatus Status { get; private set; } = AccountStatus.Active;
    
    public ICollection<Character>? Characters { get; init; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public static Account Create(long steamId)
    {
        var account = new Account { Id = Guid.NewGuid(), SteamId = steamId };
        account.DomainEvents.Add(new AccountCreatedEvent(account));
        return account;
    }
    
    public static Account Create(Account accountInfo)
    {
        var account = Create(accountInfo.SteamId);
        account.SetValues(accountInfo);
        return account;
    }

    public Account SetValues(Account accountInfo)
    {
        return this;
    }

    public void Lock()
    {
        Status = AccountStatus.Locked;
        DomainEvents.Add(new AccountLockedEvent(this));
    }
    
    public void Unlock()
    {
        Status = AccountStatus.Active;
        DomainEvents.Add(new AccountLockedEvent(this));
    }
}
