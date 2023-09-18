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

        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Location).Property(x => x.X).HasColumnName("WorldPosition_Pos_X");
        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Location).Property(x => x.Y).HasColumnName("WorldPosition_Pos_Y");
        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Location).Property(x => x.Z).HasColumnName("WorldPosition_Pos_Z");
        
        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Rotation).Property(x => x.A).HasColumnName("WorldPosition_Rot_A");
        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Rotation).Property(x => x.B).HasColumnName("WorldPosition_Rot_B");
        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Rotation).Property(x => x.C).HasColumnName("WorldPosition_Rot_C");
        builder.OwnsOne(x => x.WorldPosition).OwnsOne(x => x.Rotation).Property(x => x.D).HasColumnName("WorldPosition_Rot_D");

        builder
            .HasOne(x => x.Account)
            .WithMany(x => x.Characters)
            .HasConstraintName("FK_Account_Id");

        builder
            .HasMany(x => x.BankAccounts)
            .WithOne(x => x.OwningCharacter)
            .HasForeignKey("FK_Character_Id")
            .HasConstraintName("FK_BankAccount_Character_Id");
    }
}
