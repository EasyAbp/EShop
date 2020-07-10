using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RenamedNameToUniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EShopProductsProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "EShopOrdersOrderLines");

            migrationBuilder.DropColumn(
                name: "ProductTypeName",
                table: "EShopOrdersOrderLines");

            migrationBuilder.AddColumn<string>(
                name: "UniqueName",
                table: "EShopProductsProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDisplayName",
                table: "EShopPluginsBasketsBasketItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUniqueName",
                table: "EShopPluginsBasketsBasketItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDisplayName",
                table: "EShopOrdersOrderLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeUniqueName",
                table: "EShopOrdersOrderLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductUniqueName",
                table: "EShopOrdersOrderLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueName",
                table: "EShopProductsProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductDisplayName",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "ProductUniqueName",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "ProductDisplayName",
                table: "EShopOrdersOrderLines");

            migrationBuilder.DropColumn(
                name: "ProductTypeUniqueName",
                table: "EShopOrdersOrderLines");

            migrationBuilder.DropColumn(
                name: "ProductUniqueName",
                table: "EShopOrdersOrderLines");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EShopProductsProductTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "EShopPluginsBasketsBasketItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "EShopOrdersOrderLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeName",
                table: "EShopOrdersOrderLines",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
