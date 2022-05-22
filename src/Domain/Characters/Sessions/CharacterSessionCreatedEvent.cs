using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Characters.Sessions;

public class CharacterSessionCreatedEvent : DomainEvent
{
    public CharacterSessionCreatedEvent(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }
}
