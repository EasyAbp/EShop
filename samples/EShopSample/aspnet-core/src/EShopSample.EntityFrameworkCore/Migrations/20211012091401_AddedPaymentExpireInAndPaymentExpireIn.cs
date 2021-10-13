using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedPaymentExpireInAndPaymentExpireIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PaymentExpireIn",
                table: "EasyAbpEShopProductsProductSkus",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "PaymentExpireIn",
                table: "EasyAbpEShopProductsProducts",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentExpiration",
                table: "EasyAbpEShopOrdersOrders",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentExpireIn",
                table: "EasyAbpEShopProductsProductSkus");

            migrationBuilder.DropColumn(
                name: "PaymentExpireIn",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.DropColumn(
                name: "PaymentExpiration",
                table: "EasyAbpEShopOrdersOrders");
        }
    }
}
