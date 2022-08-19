using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    public partial class AddedProductInventoryStrategyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReducedInventoryTime",
                table: "EasyAbpEShopPluginsFlashSalesFlashSaleResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductInventoryStrategy",
                table: "EasyAbpEShopOrdersOrderLines",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReducedInventoryTime",
                table: "EasyAbpEShopPluginsFlashSalesFlashSaleResults");

            migrationBuilder.DropColumn(
                name: "ProductInventoryStrategy",
                table: "EasyAbpEShopOrdersOrderLines");
        }
    }
}
