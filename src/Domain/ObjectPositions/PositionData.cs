namespace ELifeRPG.Domain.ObjectPositions;

public class PositionData
{
    public EnfusionVector Location { get; init; } = new();

    public EnfusionQuaternion Rotation { get; init; } = new();
}
