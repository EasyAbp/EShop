using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedProductView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EasyAbpEShopProductsProducts_UniqueName",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopProductsProductViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InventoryStrategy = table.Column<int>(type: "int", nullable: false),
                    MediaResources = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    ProductGroupDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximumPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Sold = table.Column<long>(type: "bigint", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopProductsProductViews", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopProductsProducts_UniqueName",
                table: "EasyAbpEShopProductsProducts",
                column: "UniqueName");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopProductsProductViews_UniqueName",
                table: "EasyAbpEShopProductsProductViews",
                column: "UniqueName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropIndex(
                name: "IX_EasyAbpEShopProductsProducts_UniqueName",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopProductsProducts_UniqueName",
                table: "EasyAbpEShopProductsProducts",
                column: "UniqueName",
                unique: true,
                filter: "[UniqueName] IS NOT NULL");
        }
    }
}
