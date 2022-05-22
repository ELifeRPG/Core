﻿using ELifeRPG.Domain.Accounts;
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
        
        builder.Property(x => x.EnfusionIdentifier).HasColumnName("EnfusionIdentifier").IsRequired();
        builder.HasIndex(x => x.EnfusionIdentifier).HasDatabaseName("IDX_Account_EnfusionIdentifier");
        
        builder.Property(x => x.Status).HasColumnName("Status").IsRequired();
    }
}
