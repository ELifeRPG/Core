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
            .HasMany(x => x.ReceivedTransactions)
            .WithOne(x => x.Target)
            .HasForeignKey("FK_Bank_Id")
            .HasConstraintName("FK_BankAccountTransaction_Bank_Id");

        builder
            .HasMany(x => x.SentTransactions)
            .WithOne(x => x.BankAccount)
            .HasForeignKey("FK_Bank_Target_Id")
            .HasConstraintName("FK_BankAccountTransaction_Bank_Target_Id");
    }
}
