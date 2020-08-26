using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedRefundItemOrderLineToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefundItemOrderLine_EasyAbpEShopPaymentsRefundItems_RefundItemId",
                table: "RefundItemOrderLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefundItemOrderLine",
                table: "RefundItemOrderLine");

            migrationBuilder.RenameTable(
                name: "RefundItemOrderLine",
                newName: "EasyAbpEShopPaymentsRefundItemOrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_RefundItemOrderLine_RefundItemId",
                table: "EasyAbpEShopPaymentsRefundItemOrderLines",
                newName: "IX_EasyAbpEShopPaymentsRefundItemOrderLines_RefundItemId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrderLines",
                type: "decimal(20,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "EasyAbpEShopPaymentsRefundItemOrderLines",
                type: "decimal(20,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsRefundItemOrderLines",
                table: "EasyAbpEShopPaymentsRefundItemOrderLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopPaymentsRefundItemOrderLines_EasyAbpEShopPaymentsRefundItems_RefundItemId",
                table: "EasyAbpEShopPaymentsRefundItemOrderLines",
                column: "RefundItemId",
                principalTable: "EasyAbpEShopPaymentsRefundItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopPaymentsRefundItemOrderLines_EasyAbpEShopPaymentsRefundItems_RefundItemId",
                table: "EasyAbpEShopPaymentsRefundItemOrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsRefundItemOrderLines",
                table: "EasyAbpEShopPaymentsRefundItemOrderLines");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPaymentsRefundItemOrderLines",
                newName: "RefundItemOrderLine");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopPaymentsRefundItemOrderLines_RefundItemId",
                table: "RefundItemOrderLine",
                newName: "IX_RefundItemOrderLine_RefundItemId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrderLines",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "RefundItemOrderLine",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefundItemOrderLine",
                table: "RefundItemOrderLine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundItemOrderLine_EasyAbpEShopPaymentsRefundItems_RefundItemId",
                table: "RefundItemOrderLine",
                column: "RefundItemId",
                principalTable: "EasyAbpEShopPaymentsRefundItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
