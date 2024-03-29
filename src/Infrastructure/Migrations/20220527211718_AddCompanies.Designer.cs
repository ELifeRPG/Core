﻿// <auto-generated />


#nullable disable

using System;
using ELifeRPG.Application.Common;
using ELifeRPG.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
namespace ELifeRPG.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220527211718_AddCompanies")]
    partial class AddCompanies
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
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
                        .ValueGeneratedOnAdd()
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

                    b.HasKey("Id")
                        .HasName("PK_CompanyPosition_Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyPosition", (string)null);
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

            modelBuilder.Entity("ELifeRPG.Domain.Characters.Character", b =>
                {
                    b.Navigation("CompanyMemberships");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("ELifeRPG.Domain.Companies.Company", b =>
                {
                    b.Navigation("Memberships");

                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
