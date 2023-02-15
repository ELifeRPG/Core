using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Common.Exceptions;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Items;

namespace ELifeRPG.Domain.Shops;

public enum ShopType
{
    PlayerOwnedShop = 1,
    VirtualPlayerOwnedShop = 2,
    CompanyShop = 3,
}

public class Shop : EntityBase
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string DisplayName { get; init; } = null!;
    
    public ShopType Type { get; init; }
    
    public Company? OwningCompany { get; init; } = null!;
    
    public Character? OwningCharacter { get; init; } = null!;
    
    public ICollection<ShopListing>? Listings { get; set; }

    /// <summary>
    /// Instantiates a new player owned shop.
    /// </summary>
    /// <param name="name">The shop name.</param>
    /// <param name="owner">The owning character.</param>
    public Shop(string name, Character owner)
    {
        Type = ShopType.PlayerOwnedShop;
        DisplayName = name;
        OwningCharacter = owner;
    }

    public void ListItem(Item item, int amount)
    {
        if (Listings is null)
        {
            throw new ELifeInvalidOperationException();
        }
        
        Listings.Add(new ShopListing(item, amount));
    }
}
