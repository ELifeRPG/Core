namespace ELifeRPG.Domain.Shops;

public enum ShopType
{
    PlayerOwnedShop = 1,
    VirtualPlayerOwnedShop = 2,
    CompanyShop = 3,
}

public class Shop
{
    public Guid Id { get; init; }

    public string DisplayName { get; init; } = null!;
    
    public ShopType Type { get; init; }
    
    
}
