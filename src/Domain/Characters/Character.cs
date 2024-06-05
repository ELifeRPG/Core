using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters.Events;
using ELifeRPG.Domain.Characters.Sessions;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Common.Base;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Persons;
using ELifeRPG.Domain.ObjectPositions;

namespace ELifeRPG.Domain.Characters;

public class Character : EntityBase, IHasDomainEvents, IHuman
{
    private CharacterName? _name;
    private PositionData _worldPosition;
    private decimal _cash;

    internal Character()
    {
    }

    public Character(Character characterInfo)
    {
        SetValues(characterInfo);
        Person = new Person(this);
        DomainEvents.Add(new CharacterCreatedEvent(this));
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public Account? Account { get; init; }
    
    public Person? Person { get; init; }

    public CharacterName? Name
    {
        get => _name;
        init => _name = value;
    }

    public PositionData WorldPosition
    {
        get => _worldPosition;
        init => _worldPosition = value;
    }

    public decimal Cash
    {
        get => _cash;
        init => _cash = value;
    }

    public ICollection<CharacterSession>? Sessions { get; init; }
    
    public ICollection<CompanyMembership>? CompanyMemberships { get; init; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public Character SetValues(Character characterInfo)
    {
        _name = characterInfo.Name;
        _worldPosition = characterInfo.WorldPosition;
        return this;
    }
    
    public void CreateSession()
    {
        var currentSession = GetCurrentSession();
        if (currentSession is not null)
        {
            EndSession(currentSession);
        }
        
        Sessions!.Add(new CharacterSession());
        DomainEvents.Add(new CharacterSessionCreatedEvent(this));
    }

    public void EndSession()
    {
        var currentSession = GetCurrentSession();
        if (currentSession is null)
        {
            throw new InvalidOperationException();
        }
        
        EndSession(currentSession);
    }
    
    public void EndSession(CharacterSession session)
    {
        DomainEvents.Add(new CharacterSessionEndedEvent(this));
        session.End();
    }

    public CharacterSession? GetCurrentSession()
    {
        return Sessions?.SingleOrDefault(x => x.Ended is not null);
    }

    public bool HasCash(decimal amount)
    {
        return Cash >= amount;
    }
    
    public bool PayCash(decimal amount, IHasCash receiver)
    {
        if (!HasCash(amount))
        {
            return false;
        }
        
        _cash -= amount;
        receiver.ReceiveCash(amount);
        
        return true;
    }

    public void ReceiveCash(decimal amount)
    {
        _cash += amount;
    }
}
