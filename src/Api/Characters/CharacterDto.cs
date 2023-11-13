using ELifeRPG.Core.Api.ObjectPositions;

namespace ELifeRPG.Core.Api.Characters;

public class CharacterDto
{
    public string Id { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;

    public PositionDataDto WorldPosition { get; set; } = null!;
}
