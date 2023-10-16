using ELifeRPG.Domain.Shops;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Shops;

public class ShopTypeConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shop");
        
        builder.HasKey(x => x.Id).HasName("PK_Shop_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.DisplayName).HasColumnName("DisplayName");
        
        builder
            .HasOne(x => x.OwningCharacter)
            .WithMany(x => x.Shops)
            .HasForeignKey("FK_Character_Id")
            .HasConstraintName("FK_Shop_Character_Id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(x => x.OwningCompany)
            .WithMany(x => x.Shops)
            .HasForeignKey("FK_Company_Id")
            .HasConstraintName("FK_Shop_Company_Id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
