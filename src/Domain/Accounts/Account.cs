using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Accounts;

public class Account : EntityBase, IHasDomainEvents
{
    private AccountStatus _status = AccountStatus.Active;
    
    public Account()
    {
    }

    public Account(long steamId)
    {
        Id = Guid.NewGuid();
        SteamId = steamId;
        
        DomainEvents.Add(new AccountCreatedEvent(this));
    }
    
    public Guid Id { get; init; }

    public long SteamId { get; init; }

    public AccountStatus Status
    {
        get => _status;
        init => _status = value;
    }

    public ICollection<Character>? Characters { get; init; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public void Lock()
    {
        ELifeInvalidOperationException.ThrowIf(_status == AccountStatus.Locked, "Account is already locked.");
        
        _status = AccountStatus.Locked;
        DomainEvents.Add(new AccountLockedEvent(this));
    }
    
    public void Unlock()
    {
        ELifeInvalidOperationException.ThrowIf(_status == AccountStatus.Active, "Account is already active.");
        
        _status = AccountStatus.Active;
        DomainEvents.Add(new AccountUnlockedEvent(this));
    }
}
