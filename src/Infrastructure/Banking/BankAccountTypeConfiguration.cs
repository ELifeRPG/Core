using ELifeRPG.Domain.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Banking;

#pragma warning disable CS8602

public class BankAccountTypeConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.ToTable("BankAccount");
        
        builder.HasKey(x => x.Id).HasName("PK_BankAccount_Id");
        builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedNever();
        
        builder.Property(x => x.Type).HasColumnName("Type");

        builder.Property(x => x.Number).HasColumnName("Number");
        builder.HasIndex(x => x.Number).IsUnique();
        
        builder
            .HasMany(x => x.Bookings)
            .WithOne(x => x.BankAccount)
            .HasForeignKey("FK_BankAccount_Id")
            .HasConstraintName("FK_BankAccountBooking_BankAccount_Id");
    }
}
