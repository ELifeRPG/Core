using ELifeRPG.Core.Api.ObjectPositions;
using ELifeRPG.Domain.Characters;

namespace ELifeRPG.Core.Api.Characters;

public class CharacterDto
{
    public Guid Id { get; init; }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public PositionDataDto WorldPosition { get; init; } = null!;

    public static CharacterDto Create(Character character)
        => new()
        {
            Id = character.Id,
            FirstName = character.Name!.FirstName,
            LastName = character.Name!.LastName,
        };
}
