using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Characters.Sessions;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;

namespace ELifeRPG.Domain.Characters;

public class Character : EntityBase, IHasDomainEvents
{
    private CharacterName? _name;

    public Character()
    {
    }

    public Character(Character characterInfo)
    {
        SetValues(characterInfo);
        DomainEvents.Add(new CharacterCreatedEvent(this));
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    
    public Account? Account { get; init; }

    public CharacterName? Name
    {
        get => _name;
        init => _name = value;
    }

    public ICollection<CharacterSession>? Sessions { get; init; }
    
    public ICollection<CompanyMembership>? CompanyMemberships { get; init; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public Character SetValues(Character characterInfo)
    {
        _name = characterInfo.Name;
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
}
