using ELifeRPG.Domain.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Items;

public class PrefabTypeConfiguration : IEntityTypeConfiguration<Prefab>
{
    public void Configure(EntityTypeBuilder<Prefab> builder)
    {
        builder.ToTable("Prefab");
        
        builder.HasKey(x => x.Id).HasName("PK_Prefab_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder
            .HasOne(x => x.Item)
            .WithOne(x => x.Prefab)
            .HasForeignKey<Prefab>("FK_Item_Id")
            .HasConstraintName("FK_Prefab_Item_Id")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
