using ELifeRPG.Domain.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Shops;

#pragma warning disable CS8602

public class ShopTypeConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shop");
        
        builder.HasKey(x => x.Id).HasName("PK_Shop_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.DisplayName).HasColumnName("DisplayName");
    }
}
