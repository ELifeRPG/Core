namespace ELifeRPG.Domain.Items;

public class Prefab
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;
    
    public Item? Item { get; init; }
}
