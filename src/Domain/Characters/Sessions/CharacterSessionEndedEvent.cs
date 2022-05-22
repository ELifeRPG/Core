using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Characters.Sessions;

public class CharacterSessionEndedEvent : DomainEvent
{
    public CharacterSessionEndedEvent(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }
}
