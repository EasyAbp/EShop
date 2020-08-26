using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class ChangesForOrderRefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefundedAmount",
                table: "EasyAbpEShopOrdersOrders",
                newName: "RefundAmount");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EasyAbpPaymentServiceRefundItems",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "EasyAbpEShopPaymentsRefundItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EasyAbpEShopPaymentsRefundItems",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "EasyAbpEShopPaymentsRefundItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrderLines",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RefundedQuantity",
                table: "EasyAbpEShopOrdersOrderLines",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RefundItemOrderLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderLineId = table.Column<Guid>(nullable: false),
                    RefundedQuantity = table.Column<int>(nullable: false),
                    RefundAmount = table.Column<decimal>(nullable: false),
                    RefundItemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundItemOrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefundItemOrderLine_EasyAbpEShopPaymentsRefundItems_RefundItemId",
                        column: x => x.RefundItemId,
                        principalTable: "EasyAbpEShopPaymentsRefundItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefundItemOrderLine_RefundItemId",
                table: "RefundItemOrderLine",
                column: "RefundItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefundItemOrderLine");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EasyAbpPaymentServiceRefundItems");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EasyAbpEShopPaymentsRefundItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "EasyAbpEShopPaymentsRefundItems");

            migrationBuilder.DropColumn(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.DropColumn(
                name: "RefundedQuantity",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.RenameColumn(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrders",
                newName: "RefundedAmount");

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "EasyAbpEShopPaymentsRefundItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
