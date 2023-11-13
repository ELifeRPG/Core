namespace ELifeRPG.Core.Api.ObjectPositions;

public class PositionDataDto
{
    public EnfusionVectorDto Location { get; set; } = null!;
    public EnfusionQuaternionDto Rotation { get; set; } = null!;
}
