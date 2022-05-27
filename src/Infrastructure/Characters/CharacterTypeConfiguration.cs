using ELifeRPG.Domain.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Characters;

#pragma warning disable CS8602

public class CharacterTypeConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("Character");
        
        builder.HasKey(x => x.Id).HasName("PK_Character_Id");
        builder.Property(x => x.Id).HasColumnName("Id");

        builder.OwnsOne(x => x.Name).Property(x => x.FirstName).HasColumnName("FirstName").HasMaxLength(50).IsRequired();
        builder.OwnsOne(x => x.Name).Property(x => x.LastName).HasColumnName("LastName").HasMaxLength(50).IsRequired();

        builder
            .HasOne(x => x.Account)
            .WithMany(x => x.Characters)
            .HasConstraintName("FK_Account_Id");

        builder
            .HasMany(x => x.CompanyMemberships)
            .WithOne(x => x.Character)
            .HasConstraintName("FK_Character_Id");
    }
}
