using ELifeRPG.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Items;

public class ItemTypeConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item");
        
        builder.HasKey(x => x.Id).HasName("PK_Item_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.DisplayName).HasColumnName("DisplayName");

        builder
            .HasOne(x => x.Prefab)
            .WithOne(x => x.Item)
            .HasForeignKey<Item>("FK_Prefab_Id")
            .HasConstraintName("FK_Item_Prefab_Id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
