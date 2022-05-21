using ELifeRPG.Domain.Accounts;
using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Characters;

public class Character : EntityBase, IHasDomainEvents
{
    private CharacterName? _name;

    public Guid Id { get; init; }
    
    public Account? Account { get; init; }

    public CharacterName? Name
    {
        get => _name;
        init => _name = value;
    }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public static Character Create(Character characterInfo)
    {
        var character = new Character();
        character.SetValues(characterInfo);
        character.DomainEvents.Add(new CharacterCreatedEvent(character));
        return character;
    }

    public Character SetValues(Character characterInfo)
    {
        _name = characterInfo.Name;
        return this;
    }
}
