using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    public partial class AddedInventoryProviderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InventoryProviderName",
                table: "EasyAbpEShopProductsProductViews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InventoryProviderName",
                table: "EasyAbpEShopProductsProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InventoryProviderName",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "InventoryProviderName",
                table: "EasyAbpEShopProductsProducts");
        }
    }
}
