namespace ELifeRPG.Domain.Common;

public class EntityBase
{
    protected EntityBase()
    {
        Created = DateTime.UtcNow;
    }
    
    public DateTime Created { get; init; }
}
