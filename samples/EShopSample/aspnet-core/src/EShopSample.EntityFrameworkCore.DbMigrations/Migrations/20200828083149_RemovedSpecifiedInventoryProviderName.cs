using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RemovedSpecifiedInventoryProviderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecifiedInventoryProviderName",
                table: "EasyAbpEShopProductsProductSkus");

            migrationBuilder.DropColumn(
                name: "SpecifiedInventoryProviderName",
                table: "EasyAbpEShopProductsProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecifiedInventoryProviderName",
                table: "EasyAbpEShopProductsProductSkus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecifiedInventoryProviderName",
                table: "EasyAbpEShopProductsProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
