using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Characters;

public class CharacterCreatedEvent : DomainEvent
{
    public CharacterCreatedEvent(Character character)
    {
        Character = character;
    }
    
    public Character Character { get; }
}
