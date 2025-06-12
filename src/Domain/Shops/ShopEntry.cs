using ELifeRPG.Domain.Items;

namespace ELifeRPG.Domain.Shops;

public class ShopEntry
{
    public Guid Id { get; init; }

    public Shop? Shop { get; init; }

    public Item? Item { get; init; }

    public int Amount { get; init; }
}
