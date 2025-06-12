using ELifeRPG.Domain.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELifeRPG.Infrastructure.Banking;

#pragma warning disable CS8602

public class BankAccountBookingTypeConfiguration : IEntityTypeConfiguration<BankAccountBooking>
{
    public void Configure(EntityTypeBuilder<BankAccountBooking> builder)
    {
        builder.ToTable("BankAccountBooking");

        builder.HasKey(x => x.Id).HasName("PK_BankAccountBooking_Id");
        builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedNever();

        builder.Property(x => x.Type).HasColumnName("Type");
        builder.Property(x => x.Amount).HasColumnName("Amount");

        builder
            .HasOne(x => x.Source)
            .WithMany(x => x.OutgoingBookings)
            .HasForeignKey("FK_BankAccount_Id")
            .HasConstraintName("FK_BankAccount_BankAccountBooking_Id");
    }
}
