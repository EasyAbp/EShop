using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class MovedStoreIdFromPaymentToPaymentItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "EShopPaymentsPayments");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "PaymentServicePaymentItems",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "EShopPaymentsPaymentItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "PaymentServicePaymentItems");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "EShopPaymentsPaymentItems");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "EShopPaymentsPayments",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
