using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    public partial class Banking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Permissions",
                table: "CompanyPosition",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    FK_Bank_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_BankCondition_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_Character_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_Company_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_Bank_Id",
                        column: x => x.FK_Bank_Id,
                        principalTable: "Bank",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankAccount_BankCondition_Id",
                        column: x => x.FK_BankCondition_Id,
                        principalTable: "BankCondition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankAccount_Character_Id",
                        column: x => x.FK_Character_Id,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankAccount_Company_Id",
                        column: x => x.FK_Company_Id,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankAccountTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FK_Bank_Target_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Bank_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Fees = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountTransaction_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccountTransaction_Bank_Id",
                        column: x => x.FK_Bank_Id,
                        principalTable: "BankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankAccountTransaction_Bank_Target_Id",
                        column: x => x.FK_Bank_Target_Id,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BankAccount_FK_Character_Id",
                table: "BankAccount",
                column: "FK_Character_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_FK_Company_Id",
                table: "BankAccount",
                column: "FK_Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_Number",
                table: "BankAccount",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransaction_FK_Bank_Id",
                table: "BankAccountTransaction",
                column: "FK_Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransaction_FK_Bank_Target_Id",
                table: "BankAccountTransaction",
                column: "FK_Bank_Target_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankCondition_FK_Bank_Id",
                table: "BankCondition",
                column: "FK_Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Code",
                table: "Country",
                column: "Code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountTransaction");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankCondition");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "CompanyPosition");
        }
    }
}
