using ELifeRPG.Domain.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Banking;

#pragma warning disable CS8602

public class BankAccountTransactionTypeConfiguration : IEntityTypeConfiguration<BankAccountTransaction>
{
    public void Configure(EntityTypeBuilder<BankAccountTransaction> builder)
    {
        builder.ToTable("BankAccountTransaction");
        
        builder.HasKey(x => x.Id).HasName("PK_BankAccountTransaction_Id");
        builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedNever();
        
        builder.Property(x => x.Type).HasColumnName("Type");
        builder.Property(x => x.Amount).HasColumnName("Amount");
        builder.Property(x => x.Fees).HasColumnName("Fees");
    }
}
