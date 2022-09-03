using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    public partial class Banking2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountTransaction");

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

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBooking_FK_BankAccount_Id",
                table: "BankAccountBooking",
                column: "FK_BankAccount_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountBooking_SourceId",
                table: "BankAccountBooking",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccountBooking");

            migrationBuilder.CreateTable(
                name: "BankAccountTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Bank_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_Bank_Target_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fees = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_BankAccountTransaction_FK_Bank_Id",
                table: "BankAccountTransaction",
                column: "FK_Bank_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccountTransaction_FK_Bank_Target_Id",
                table: "BankAccountTransaction",
                column: "FK_Bank_Target_Id");
        }
    }
}
