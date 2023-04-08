using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    /// <inheritdoc />
    public partial class ImplementedProductDiscounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "EasyAbpEShopPluginsBasketsBasketItems");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                newName: "TotalPriceWithoutDiscount");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                newName: "PriceWithoutDiscount");

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumPriceWithoutDiscount",
                table: "EasyAbpEShopProductsProductViews",
                type: "decimal(20,8)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumPriceWithoutDiscount",
                table: "EasyAbpEShopProductsProductViews",
                type: "decimal(20,8)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderDiscountPreviews",
                table: "EasyAbpEShopProductsProductViews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDiscounts",
                table: "EasyAbpEShopProductsProductViews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderDiscountPreviews",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductDiscounts",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumPriceWithoutDiscount",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "MinimumPriceWithoutDiscount",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "OrderDiscountPreviews",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "ProductDiscounts",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "OrderDiscountPreviews",
                table: "EasyAbpEShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "ProductDiscounts",
                table: "EasyAbpEShopPluginsBasketsBasketItems");

            migrationBuilder.RenameColumn(
                name: "TotalPriceWithoutDiscount",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "PriceWithoutDiscount",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                type: "decimal(20,8)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
