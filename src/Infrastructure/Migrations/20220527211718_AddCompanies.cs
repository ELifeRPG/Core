using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    public partial class AddCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ordering = table.Column<int>(type: "integer", nullable: false),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyMembership");

            migrationBuilder.DropTable(
                name: "CompanyPosition");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
