using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterWorldPositionAsJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorldPosition",
                table: "Character",
                type: "jsonb",
                nullable: false,
                defaultValue: "{\r\n  \"location\": {\r\n    \"x\": 0.0,\r\n    \"y\": 0.0,\r\n    \"z\": 0.0\r\n  },\r\n  \"rotation\": {\r\n    \"a\": 0.0,\r\n    \"b\": 0.0,\r\n    \"c\": 0.0,\r\n    \"d\": 0.0\r\n  }\r\n}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorldPosition",
                table: "Character");
        }
    }
}
