using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Persons;

#pragma warning disable CS8602

public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Person");
        
        builder.HasKey(x => x.Id).HasName("PK_Person_Id");
        builder.Property(x => x.Id).HasColumnName("Id");

        builder
            .HasOne(x => x.Character)
            .WithOne(x => x.Person)
            .HasForeignKey<Person>("FK_Person_Id")
            .HasConstraintName("FK_Character_Person_Id");
        
        builder
            .HasOne(x => x.Company)
            .WithOne(x => x.Person)
            .HasForeignKey<Company>("FK_Person_Id")
            .HasConstraintName("FK_Company_Person_Id");
    }
}
