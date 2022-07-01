using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    public partial class AddedCurrencyProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssetPeriods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssetCategoryPeriods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssetCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssets");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssetPeriods");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssetCategoryPeriods");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPluginsBookingProductAssetCategories");
        }
    }
}
