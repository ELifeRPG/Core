namespace ELifeRPG.Domain.ObjectPositions;

public class PositionData
{
    public EnfusionVector Location { get; init; } = null!;
    public EnfusionQuaternion Rotation { get; init; } = null!;
}
