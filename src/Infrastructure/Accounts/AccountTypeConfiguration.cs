using ELifeRPG.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Accounts;

#pragma warning disable CS8602

public class AccountTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Account");
        
        builder.HasKey(x => x.Id).HasName("PK_Account_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.DiscordId).HasColumnName("DiscordId").IsRequired();
        builder.HasIndex(x => x.DiscordId).HasDatabaseName("IDX_Account_DiscordId");
        
        builder.Property(x => x.BohemiaId).HasColumnName("BohemiaId");
        builder.HasIndex(x => x.BohemiaId).HasDatabaseName("IDX_Account_BohemiaId");
        
        builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
    }
}
