using ELifeRPG.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Companies;

#pragma warning disable CS8602

public class CompanyPositionTypeConfiguration : IEntityTypeConfiguration<CompanyPosition>
{
    public void Configure(EntityTypeBuilder<CompanyPosition> builder)
    {
        builder.ToTable("CompanyPosition");
        
        builder.HasKey(x => x.Id).HasName("PK_CompanyPosition_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Ordering).HasColumnName("Ordering");
    }
}
