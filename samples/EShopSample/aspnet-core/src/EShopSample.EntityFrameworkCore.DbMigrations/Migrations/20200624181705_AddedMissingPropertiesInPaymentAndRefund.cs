using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedMissingPropertiesInPaymentAndRefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledTime",
                table: "EShopPaymentsRefunds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedTime",
                table: "EShopPaymentsRefunds",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingRefundAmount",
                table: "EShopPaymentsPayments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingRefundAmount",
                table: "EShopPaymentsPaymentItems",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelledTime",
                table: "EShopPaymentsRefunds");

            migrationBuilder.DropColumn(
                name: "CompletedTime",
                table: "EShopPaymentsRefunds");

            migrationBuilder.DropColumn(
                name: "PendingRefundAmount",
                table: "EShopPaymentsPayments");

            migrationBuilder.DropColumn(
                name: "PendingRefundAmount",
                table: "EShopPaymentsPaymentItems");
        }
    }
}
