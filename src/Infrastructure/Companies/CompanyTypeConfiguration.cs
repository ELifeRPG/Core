using ELifeRPG.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Companies;

#pragma warning disable CS8602

public class CompanyTypeConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Company");
        
        builder.HasKey(x => x.Id).HasName("PK_Company_Id");
        builder.Property(x => x.Id).HasColumnName("Id");
        
        builder.Property(x => x.Name).HasColumnName("Name");

        builder
            .HasMany(x => x.Positions)
            .WithOne(x => x.Company)
            .HasConstraintName("FK_Position_Id");
        
        builder
            .HasMany(x => x.Memberships)
            .WithOne(x => x.Company)
            .HasConstraintName("FK_CompanyMembership_Id");

        builder
            .HasMany(x => x.BankAccounts)
            .WithOne(x => x.OwningCompany)
            .HasForeignKey("FK_Company_Id")
            .HasConstraintName("FK_BankAccount_Company_Id");
    }
}
