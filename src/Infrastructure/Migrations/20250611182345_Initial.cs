using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    BohemiaId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    WorldPosition = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{\n  \"location\": {\n    \"x\": 0.0,\n    \"y\": 0.0,\n    \"z\": 0.0\n  },\n  \"rotation\": {\n    \"a\": 0.0,\n    \"b\": 0.0,\n    \"c\": 0.0,\n    \"d\": 0.0\n  }\n}"),
                    Cash = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_Id",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Country_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bank_Country_Id",
                        column: x => x.FK_Country_Id,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterSession",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: true),
                    Ended = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSession_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Id",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FK_Person_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Person_Id",
                        column: x => x.FK_Person_Id,
                        principalTable: "Character",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Bank_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    TransactionFeeBase = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionFeeMultiplier = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCondition_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCondition_Bank_Id",
                        column: x => x.FK_Bank_Id,
                        principalTable: "Bank",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Person_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Person_Id",
                        column: x => x.FK_Person_Id,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    FK_Bank_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_BankCondition_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_Person_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankCondition_Id",
                        column: x => x.FK_BankCondition_Id,
                        principalTable: "BankCondition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankAccount_Bank_Id",
                        column: x => x.FK_Bank_Id,
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Person_BankAccount_Id",
                        column: x => x.FK_Person_Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ordering = table.Column<int>(type: "integer", nullable: false),
                    Permissions = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPosition_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Position_Id",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountBooking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FK_BankAccount_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: true),
                    Purpose = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountBooking_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountBooking_BankAccount_Id",
                        column: x => x.FK_BankAccount_Id,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccountBooking_BankAccount_SourceId",
                        column: x => x.SourceId,
                        principalTable: "BankAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyMembership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMembership_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Id",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyMembership_CompanyPosition_PositionId",
                        column: x => x.PositionId,
                        principalTable: "CompanyPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyMembership_Id",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Account_BohemiaId",
                table: "Account",
                column: "BohemiaId");

            migrationBuilder.CreateIndex(
                name: "IDX_Account_SteamId",
                table: "Account",
                column: "SteamId");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_FK_Country_Id",
                table: "Bank",
                column: "FK_Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bank_Number",
                table: "Bank",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_FK_Bank_Id",
                table: "BankAccount",
                column: "FK_Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_FK_BankCondition_Id",
                table: "BankAccount",
                column: "FK_BankCondition_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_FK_Person_Id",
                table: "BankAccount",
                column: "FK_Person_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_Number",
                table: "BankAccount",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBooking_FK_BankAccount_Id",
                table: "BankAccountBooking",
                column: "FK_BankAccount_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBooking_SourceId",
                table: "BankAccountBooking",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_BankCondition_FK_Bank_Id",
                table: "BankCondition",
                column: "FK_Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Character_AccountId",
                table: "Character",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSession_CharacterId",
                table: "CharacterSession",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_FK_Person_Id",
                table: "Company",
                column: "FK_Person_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMembership_CharacterId",
                table: "CompanyMembership",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMembership_CompanyId",
                table: "CompanyMembership",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMembership_PositionId",
                table: "CompanyMembership",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPosition_CompanyId",
                table: "CompanyPosition",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                table: "Country",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_FK_Person_Id",
                table: "Person",
                column: "FK_Person_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountBooking");

            migrationBuilder.DropTable(
                name: "CharacterSession");

            migrationBuilder.DropTable(
                name: "CompanyMembership");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "CompanyPosition");

            migrationBuilder.DropTable(
                name: "BankCondition");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
