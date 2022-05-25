using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    public partial class AddSteamId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Account_EnfusionIdentifier",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "EnfusionIdentifier",
                table: "Account");

            migrationBuilder.AddColumn<long>(
                name: "SteamId",
                table: "Account",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IDX_Account_SteamId",
                table: "Account",
                column: "SteamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_Account_SteamId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "SteamId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "EnfusionIdentifier",
                table: "Account",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IDX_Account_EnfusionIdentifier",
                table: "Account",
                column: "EnfusionIdentifier");
        }
    }
}
