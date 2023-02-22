using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELifeRPG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    FK_Prefab_Id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    FK_Company_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    FK_Character_Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shop_Character_Id",
                        column: x => x.FK_Character_Id,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shop_Company_Id",
                        column: x => x.FK_Company_Id,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prefab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FK_Item_Id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefab_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prefab_Item_Id",
                        column: x => x.FK_Item_Id,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ShopListing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Shop_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FK_Item_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopListing_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopListing_Item_Id",
                        column: x => x.FK_Item_Id,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopListing_Shop_Id",
                        column: x => x.FK_Shop_Id,
                        principalTable: "Shop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prefab_FK_Item_Id",
                table: "Prefab",
                column: "FK_Item_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shop_FK_Character_Id",
                table: "Shop",
                column: "FK_Character_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_FK_Company_Id",
                table: "Shop",
                column: "FK_Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShopListing_FK_Item_Id",
                table: "ShopListing",
                column: "FK_Item_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShopListing_FK_Shop_Id",
                table: "ShopListing",
                column: "FK_Shop_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prefab");

            migrationBuilder.DropTable(
                name: "ShopListing");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Shop");
        }
    }
}
