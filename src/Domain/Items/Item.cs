using ELifeRPG.Domain.Shops;

namespace ELifeRPG.Domain.Items;

public class Item
{
    public Guid Id { get; init; }
    
    public Prefab? Prefab { get; init; }

    public string DisplayName { get; init; } = null!;
    
    public ICollection<ShopListing>? ShopListings { get; init; }
}
