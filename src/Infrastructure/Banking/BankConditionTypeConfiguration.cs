using ELifeRPG.Domain.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Banking;

#pragma warning disable CS8602

public class BankConditionTypeConfiguration : IEntityTypeConfiguration<BankCondition>
{
    public void Configure(EntityTypeBuilder<BankCondition> builder)
    {
        builder.ToTable("BankCondition");
        
        builder.HasKey(x => x.Id).HasName("PK_BankCondition_Id");
        builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedNever();
        
        builder.Property(x => x.TransactionFeeBase).HasColumnName("TransactionFeeBase");
        builder.Property(x => x.TransactionFeeMultiplier).HasColumnName("TransactionFeeMultiplier");
        
        builder
            .HasMany(x => x.BankAccounts)
            .WithOne(x => x.BankCondition)
            .HasForeignKey("FK_BankCondition_Id")
            .HasConstraintName("FK_BankAccount_BankCondition_Id");
    }
}
