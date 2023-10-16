using ELifeRPG.Domain.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Shops;

public class ShopListingTypeConfiguration : IEntityTypeConfiguration<ShopListing>
{
    public void Configure(EntityTypeBuilder<ShopListing> builder)
    {
        builder.ToTable("ShopListing");
        
        builder.HasKey(x => x.Id).HasName("PK_ShopListing_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.Amount).HasColumnName("Amount");

        builder
            .HasOne(x => x.Shop)
            .WithMany(x => x.Listings)
            .HasForeignKey("FK_Shop_Id")
            .HasConstraintName("FK_ShopListing_Shop_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(x => x.Item)
            .WithMany(x => x.ShopListings)
            .HasForeignKey("FK_Item_Id")
            .HasConstraintName("FK_ShopListing_Item_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
