using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RenamedCancelledToCanceled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CancelledTime",
                table: "PaymentServiceRefunds",
                newName: "CanceledTime");

            migrationBuilder.RenameColumn(
                name: "CancelledTime",
                table: "PaymentServicePayments",
                newName: "CanceledTime");

            migrationBuilder.RenameColumn(
                name: "CancelledTime",
                table: "EShopPaymentsRefunds",
                newName: "CanceledTime");

            migrationBuilder.RenameColumn(
                name: "CancelledTime",
                table: "EShopPaymentsPayments",
                newName: "CanceledTime");

            migrationBuilder.RenameColumn(
                name: "CancelledTime",
                table: "EShopOrdersOrders",
                newName: "CanceledTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanceledTime",
                table: "PaymentServiceRefunds",
                newName: "CancelledTime");

            migrationBuilder.RenameColumn(
                name: "CanceledTime",
                table: "PaymentServicePayments",
                newName: "CancelledTime");

            migrationBuilder.RenameColumn(
                name: "CanceledTime",
                table: "EShopPaymentsRefunds",
                newName: "CancelledTime");

            migrationBuilder.RenameColumn(
                name: "CanceledTime",
                table: "EShopPaymentsPayments",
                newName: "CancelledTime");

            migrationBuilder.RenameColumn(
                name: "CanceledTime",
                table: "EShopOrdersOrders",
                newName: "CancelledTime");
        }
    }
}
