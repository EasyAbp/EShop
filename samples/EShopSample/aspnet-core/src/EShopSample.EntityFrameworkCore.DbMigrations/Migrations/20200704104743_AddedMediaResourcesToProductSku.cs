using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedMediaResourcesToProductSku : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaResources",
                table: "EShopProductsProductSkus",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EShopPluginsBasketsBasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    BasketName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductSkuId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    MediaResources = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    SkuDescription = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopPluginsBasketsBasketItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EShopPluginsBasketsBasketItems_UserId",
                table: "EShopPluginsBasketsBasketItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "MediaResources",
                table: "EShopProductsProductSkus");
        }
    }
}
