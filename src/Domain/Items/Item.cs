namespace ELifeRPG.Domain.Items;

public class Item
{
    public Guid Id { get; init; }
    
    public Prefab? Prefab { get; init; }
}
