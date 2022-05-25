namespace ELifeRPG.Core.Api.Models;

public class SessionDto
{
    public long? SteamId { get; init; }
    
    public Guid? AccountId { get; init; }
}
