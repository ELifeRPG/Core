using ELifeRPG.Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Countries;

#pragma warning disable CS8602

public class CountryTypeConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Country");

        builder.HasKey(x => x.Id).HasName("PK_Country_Id");
        builder.Property(x => x.Id).HasColumnName("Id");

        builder.Property(x => x.Code).HasColumnName("Code");
        builder.HasIndex(x => x.Code).IsUnique();

        builder
            .HasMany(x => x.Banks)
            .WithOne(x => x.Country)
            .HasForeignKey("FK_Country_Id")
            .HasConstraintName("FK_Bank_Country_Id");
    }
}
