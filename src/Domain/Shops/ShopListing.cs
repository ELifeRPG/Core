using ELifeRPG.Domain.Items;

namespace ELifeRPG.Domain.Shops;

public class ShopListing
{
    internal ShopListing()
    {
    }
    
    public ShopListing(Item item, int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Value needs to be greater zero.");
        }
        
        Item = item;
        Amount = amount;
    }
    
    public Guid Id { get; init; }
    
    public Shop? Shop { get; init; }
    
    public Item? Item { get; init; }
    
    public int Amount { get; init; }
}
