using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Pos_X",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Pos_Y",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Pos_Z",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Rot_A",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Rot_B",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Rot_C",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WorldPosition_Rot_D",
                table: "Character",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorldPosition_Pos_X",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WorldPosition_Pos_Y",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WorldPosition_Pos_Z",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WorldPosition_Rot_A",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WorldPosition_Rot_B",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WorldPosition_Rot_C",
                table: "Character");

            migrationBuilder.DropColumn(
                name: "WorldPosition_Rot_D",
                table: "Character");
        }
    }
}
