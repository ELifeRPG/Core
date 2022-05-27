using ELifeRPG.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Companies;

#pragma warning disable CS8602

public class CompanyMembershipTypeConfiguration : IEntityTypeConfiguration<CompanyMembership>
{
    public void Configure(EntityTypeBuilder<CompanyMembership> builder)
    {
        builder.ToTable("CompanyMembership");
        
        builder.HasKey(x => x.Id).HasName("PK_CompanyMembership_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
    }
}
