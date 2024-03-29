﻿using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters.Sessions;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.ObjectPositions;

namespace ELifeRPG.Domain.Characters;

public class Character : EntityBase, IHasDomainEvents
{
    private CharacterName? _name;
    private PositionData _worldPosition;

    internal Character()
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

    public PositionData WorldPosition
    {
        get => _worldPosition;
        init => _worldPosition = value;
    }

    public ICollection<CharacterSession>? Sessions { get; init; }
    
    public ICollection<CompanyMembership>? CompanyMemberships { get; init; }
    
    public ICollection<BankAccount>? BankAccounts { get; init; }

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
}
