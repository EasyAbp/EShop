using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    public partial class AddedEntitiesInBookingPlugins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPluginsBookingProductAssetCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSkuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(20,8)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPluginsBookingProductAssetCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPluginsBookingProductAssets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductSkuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(20,8)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPluginsBookingProductAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPluginsBookingProductAssetCategoryPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    ProductAssetCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPluginsBookingProductAssetCategoryPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EasyAbpEShopPluginsBookingProductAssetCategoryPeriods_EasyAbpEShopPluginsBookingProductAssetCategories_ProductAssetCategoryId",
                        column: x => x.ProductAssetCategoryId,
                        principalTable: "EasyAbpEShopPluginsBookingProductAssetCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPluginsBookingProductAssetPeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    ProductAssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPluginsBookingProductAssetPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EasyAbpEShopPluginsBookingProductAssetPeriods_EasyAbpEShopPluginsBookingProductAssets_ProductAssetId",
                        column: x => x.ProductAssetId,
                        principalTable: "EasyAbpEShopPluginsBookingProductAssets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopPluginsBookingProductAssetCategoryPeriods_ProductAssetCategoryId",
                table: "EasyAbpEShopPluginsBookingProductAssetCategoryPeriods",
                column: "ProductAssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopPluginsBookingProductAssetPeriods_ProductAssetId",
                table: "EasyAbpEShopPluginsBookingProductAssetPeriods",
                column: "ProductAssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpEShopPluginsBookingProductAssetCategoryPeriods");

            migrationBuilder.DropTable(
                name: "EasyAbpEShopPluginsBookingProductAssetPeriods");

            migrationBuilder.DropTable(
                name: "EasyAbpEShopPluginsBookingProductAssetCategories");

            migrationBuilder.DropTable(
                name: "EasyAbpEShopPluginsBookingProductAssets");
        }
    }
}
