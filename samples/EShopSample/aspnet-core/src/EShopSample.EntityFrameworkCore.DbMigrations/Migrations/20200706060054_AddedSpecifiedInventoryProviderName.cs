using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedSpecifiedInventoryProviderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecifiedInventoryProviderName",
                table: "EShopProductsProductSkus",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecifiedInventoryProviderName",
                table: "EShopProductsProducts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecifiedInventoryProviderName",
                table: "EShopProductsProductSkus");

            migrationBuilder.DropColumn(
                name: "SpecifiedInventoryProviderName",
                table: "EShopProductsProducts");
        }
    }
}
