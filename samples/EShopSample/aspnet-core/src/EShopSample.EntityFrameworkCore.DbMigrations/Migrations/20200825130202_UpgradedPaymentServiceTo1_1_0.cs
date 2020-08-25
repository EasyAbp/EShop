using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class UpgradedPaymentServiceTo1_1_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentItemId",
                table: "EasyAbpPaymentServiceRefunds");

            migrationBuilder.DropColumn(
                name: "PaymentItemId",
                table: "EasyAbpEShopPaymentsRefunds");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "EasyAbpEShopPaymentsRefunds");

            migrationBuilder.AddColumn<string>(
                name: "DisplayReason",
                table: "EasyAbpPaymentServiceRefunds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayReason",
                table: "EasyAbpEShopPaymentsRefunds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancellationReason",
                table: "EasyAbpEShopOrdersOrders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EasyAbpPaymentServiceRefundItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    PaymentItemId = table.Column<Guid>(nullable: false),
                    RefundAmount = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    CustomerRemark = table.Column<string>(nullable: true),
                    StaffRemark = table.Column<string>(nullable: true),
                    RefundId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpPaymentServiceRefundItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EasyAbpPaymentServiceRefundItems_EasyAbpPaymentServiceRefunds_RefundId",
                        column: x => x.RefundId,
                        principalTable: "EasyAbpPaymentServiceRefunds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpPaymentServiceRefundItems_RefundId",
                table: "EasyAbpPaymentServiceRefundItems",
                column: "RefundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpPaymentServiceRefundItems");

            migrationBuilder.DropColumn(
                name: "DisplayReason",
                table: "EasyAbpPaymentServiceRefunds");

            migrationBuilder.DropColumn(
                name: "DisplayReason",
                table: "EasyAbpEShopPaymentsRefunds");

            migrationBuilder.DropColumn(
                name: "CancellationReason",
                table: "EasyAbpEShopOrdersOrders");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentItemId",
                table: "EasyAbpPaymentServiceRefunds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentItemId",
                table: "EasyAbpEShopPaymentsRefunds",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "EasyAbpEShopPaymentsRefunds",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
