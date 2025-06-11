using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SteamIdToDiscordId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SteamId",
                table: "Account",
                newName: "DiscordId");

            migrationBuilder.RenameIndex(
                name: "IDX_Account_SteamId",
                table: "Account",
                newName: "IDX_Account_DiscordId");

            migrationBuilder.AlterColumn<string>(
                name: "WorldPosition",
                table: "Character",
                type: "jsonb",
                nullable: false,
                defaultValue: "{\n  \"location\": {\n    \"x\": 0.0,\n    \"y\": 0.0,\n    \"z\": 0.0\n  },\n  \"rotation\": {\n    \"a\": 0.0,\n    \"b\": 0.0,\n    \"c\": 0.0,\n    \"d\": 0.0\n  }\n}",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldDefaultValue: "{\r\n  \"location\": {\r\n    \"x\": 0.0,\r\n    \"y\": 0.0,\r\n    \"z\": 0.0\r\n  },\r\n  \"rotation\": {\r\n    \"a\": 0.0,\r\n    \"b\": 0.0,\r\n    \"c\": 0.0,\r\n    \"d\": 0.0\r\n  }\r\n}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscordId",
                table: "Account",
                newName: "SteamId");

            migrationBuilder.RenameIndex(
                name: "IDX_Account_DiscordId",
                table: "Account",
                newName: "IDX_Account_SteamId");

            migrationBuilder.AlterColumn<string>(
                name: "WorldPosition",
                table: "Character",
                type: "jsonb",
                nullable: false,
                defaultValue: "{\r\n  \"location\": {\r\n    \"x\": 0.0,\r\n    \"y\": 0.0,\r\n    \"z\": 0.0\r\n  },\r\n  \"rotation\": {\r\n    \"a\": 0.0,\r\n    \"b\": 0.0,\r\n    \"c\": 0.0,\r\n    \"d\": 0.0\r\n  }\r\n}",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldDefaultValue: "{\n  \"location\": {\n    \"x\": 0.0,\n    \"y\": 0.0,\n    \"z\": 0.0\n  },\n  \"rotation\": {\n    \"a\": 0.0,\n    \"b\": 0.0,\n    \"c\": 0.0,\n    \"d\": 0.0\n  }\n}");
        }
    }
}
