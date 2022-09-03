﻿// <auto-generated />
using System;
using ELifeRPG.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220826125617_Banking2")]
    partial class Banking2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ELifeRPG.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("Status");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint")
                        .HasColumnName("SteamId");

                    b.HasKey("Id")
                        .HasName("PK_Account_Id");

                    b.HasIndex("SteamId")
                        .HasDatabaseName("IDX_Account_SteamId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FK_Country_Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("Number");

                    b.HasKey("Id")
                        .HasName("PK_Bank_Id");

                    b.HasIndex("FK_Country_Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Bank", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FK_BankCondition_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FK_Bank_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FK_Character_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FK_Company_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Number");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("Type");

                    b.HasKey("Id")
                        .HasName("PK_BankAccount_Id");

                    b.HasIndex("FK_BankCondition_Id");

                    b.HasIndex("FK_Bank_Id");

                    b.HasIndex("FK_Character_Id");

                    b.HasIndex("FK_Company_Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("BankAccount", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankAccountBooking", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FK_BankAccount_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Purpose")
                        .HasColumnType("text");

                    b.Property<Guid?>("SourceId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("Type");

                    b.HasKey("Id")
                        .HasName("PK_BankAccountBooking_Id");

                    b.HasIndex("FK_BankAccount_Id");

                    b.HasIndex("SourceId");

                    b.ToTable("BankAccountBooking", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankCondition", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("FK_Bank_Id")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TransactionFeeBase")
                        .HasColumnType("numeric")
                        .HasColumnName("TransactionFeeBase");

                    b.Property<decimal>("TransactionFeeMultiplier")
                        .HasColumnType("numeric")
                        .HasColumnName("TransactionFeeMultiplier");

                    b.HasKey("Id")
                        .HasName("PK_BankCondition_Id");

                    b.HasIndex("FK_Bank_Id");

                    b.ToTable("BankCondition", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Characters.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_Character_Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Character", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Characters.Sessions.CharacterSession", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid?>("CharacterId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Ended")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id")
                        .HasName("PK_CharacterSession_Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterSession", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.HasKey("Id")
                        .HasName("PK_Company_Id");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.CompanyMembership", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("CharacterId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("PK_CompanyMembership_Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PositionId");

                    b.ToTable("CompanyMembership", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.CompanyPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<int>("Ordering")
                        .HasColumnType("integer")
                        .HasColumnName("Ordering");

                    b.Property<int>("Permissions")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("PK_CompanyPosition_Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyPosition", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Countries.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Code");

                    b.HasKey("Id")
                        .HasName("PK_Country_Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Country", (string)null);
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.Bank", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Countries.Country", "Country")
                        .WithMany("Banks")
                        .HasForeignKey("FK_Country_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Bank_Country_Id");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankAccount", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Banking.BankCondition", "BankCondition")
                        .WithMany("BankAccounts")
                        .HasForeignKey("FK_BankCondition_Id")
                        .HasConstraintName("FK_BankAccount_BankCondition_Id");

                    b.HasOne("ELifeRPG.Domain.Banking.Bank", "Bank")
                        .WithMany("Accounts")
                        .HasForeignKey("FK_Bank_Id")
                        .HasConstraintName("FK_BankAccount_Bank_Id");

                    b.HasOne("ELifeRPG.Domain.Characters.Character", "OwningCharacter")
                        .WithMany("BankAccounts")
                        .HasForeignKey("FK_Character_Id")
                        .HasConstraintName("FK_BankAccount_Character_Id");

                    b.HasOne("ELifeRPG.Domain.Companies.Company", "OwningCompany")
                        .WithMany("BankAccounts")
                        .HasForeignKey("FK_Company_Id")
                        .HasConstraintName("FK_BankAccount_Company_Id");

                    b.Navigation("Bank");

                    b.Navigation("BankCondition");

                    b.Navigation("OwningCharacter");

                    b.Navigation("OwningCompany");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankAccountBooking", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Banking.BankAccount", "BankAccount")
                        .WithMany("Bookings")
                        .HasForeignKey("FK_BankAccount_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BankAccountBooking_BankAccount_Id");

                    b.HasOne("ELifeRPG.Domain.Banking.BankAccount", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");

                    b.Navigation("BankAccount");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankCondition", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Banking.Bank", "Bank")
                        .WithMany("Conditions")
                        .HasForeignKey("FK_Bank_Id")
                        .HasConstraintName("FK_BankCondition_Bank_Id");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Characters.Character", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Accounts.Account", "Account")
                        .WithMany("Characters")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Account_Id");

                    b.OwnsOne("ELifeRPG.Domain.Characters.CharacterName", "Name", b1 =>
                        {
                            b1.Property<Guid>("CharacterId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("CharacterId");

                            b1.ToTable("Character");

                            b1.WithOwner()
                                .HasForeignKey("CharacterId");
                        });

                    b.Navigation("Account");

                    b.Navigation("Name");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Characters.Sessions.CharacterSession", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Characters.Character", "Character")
                        .WithMany("Sessions")
                        .HasForeignKey("CharacterId")
                        .HasConstraintName("FK_Character_Id");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.CompanyMembership", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Characters.Character", "Character")
                        .WithMany("CompanyMemberships")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Character_Id");

                    b.HasOne("ELifeRPG.Domain.Companies.Company", "Company")
                        .WithMany("Memberships")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CompanyMembership_Id");

                    b.HasOne("ELifeRPG.Domain.Companies.CompanyPosition", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Company");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.CompanyPosition", b =>
                {
                    b.HasOne("ELifeRPG.Domain.Companies.Company", "Company")
                        .WithMany("Positions")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Position_Id");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Accounts.Account", b =>
                {
                    b.Navigation("Characters");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.Bank", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Conditions");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankAccount", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Banking.BankCondition", b =>
                {
                    b.Navigation("BankAccounts");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Characters.Character", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("CompanyMemberships");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.Company", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("Memberships");

                    b.Navigation("Positions");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Countries.Country", b =>
                {
                    b.Navigation("Banks");
                });
#pragma warning restore 612, 618
        }
    }
}
