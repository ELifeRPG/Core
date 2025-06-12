using ELifeRPG.Domain.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Banking;

#pragma warning disable CS8602

public class BankTypeConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder.ToTable("Bank");

        builder.HasKey(x => x.Id).HasName("PK_Bank_Id");
        builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedNever();

        builder.Property(x => x.Number).HasColumnName("Number");
        builder.HasIndex(x => x.Number).IsUnique();

        builder
            .HasMany(x => x.Conditions)
            .WithOne(x => x.Bank)
            .HasForeignKey("FK_Bank_Id")
            .HasConstraintName("FK_BankCondition_Bank_Id");

        builder
            .HasMany(x => x.Accounts)
            .WithOne(x => x.Bank)
            .HasForeignKey("FK_Bank_Id")
            .HasConstraintName("FK_BankAccount_Bank_Id");
    }
}
