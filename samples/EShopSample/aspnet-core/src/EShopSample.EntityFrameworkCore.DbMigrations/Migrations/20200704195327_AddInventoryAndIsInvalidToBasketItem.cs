using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddInventoryAndIsInvalidToBasketItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "EShopPluginsBasketsBasketItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsInvalid",
                table: "EShopPluginsBasketsBasketItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "IsInvalid",
                table: "EShopPluginsBasketsBasketItems");
        }
    }
}
