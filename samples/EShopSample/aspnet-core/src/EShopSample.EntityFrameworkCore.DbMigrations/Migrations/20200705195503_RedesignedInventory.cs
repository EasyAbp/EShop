using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RedesignedInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "EShopProductsProductSkus");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "EShopProductsProductSkus");

            migrationBuilder.CreateTable(
                name: "EShopProductsProductInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductSkuId = table.Column<Guid>(nullable: false),
                    Inventory = table.Column<int>(nullable: false),
                    Sold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopProductsProductInventories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsProductInventories_ProductSkuId",
                table: "EShopProductsProductInventories",
                column: "ProductSkuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EShopProductsProductInventories");

            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "EShopProductsProductSkus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sold",
                table: "EShopProductsProductSkus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
