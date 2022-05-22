using ELifeRPG.Domain.Characters.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Characters;

public class CharacterSessionTypeConfiguration : IEntityTypeConfiguration<CharacterSession>
{
    public void Configure(EntityTypeBuilder<CharacterSession> builder)
    {
        builder.ToTable("CharacterSession");
        builder.HasKey(x => x.Id).HasName("PK_CharacterSession_Id");
        builder.Property(x => x.Id).HasColumnName("Id");

        builder
            .HasOne(x => x.Character)
            .WithMany(x => x.Sessions)
            .HasConstraintName("FK_Character_Id");
    }
}
