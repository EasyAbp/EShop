using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedRefundItemToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefundItem_EasyAbpEShopPaymentsRefunds_RefundId",
                table: "RefundItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefundItem",
                table: "RefundItem");

            migrationBuilder.RenameTable(
                name: "RefundItem",
                newName: "EasyAbpEShopPaymentsRefundItems");

            migrationBuilder.RenameIndex(
                name: "IX_RefundItem_RefundId",
                table: "EasyAbpEShopPaymentsRefundItems",
                newName: "IX_EasyAbpEShopPaymentsRefundItems_RefundId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "EasyAbpEShopPaymentsRefundItems",
                type: "decimal(20,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EasyAbpEShopPaymentsRefundItems",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsRefundItems",
                table: "EasyAbpEShopPaymentsRefundItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopPaymentsRefundItems_EasyAbpEShopPaymentsRefunds_RefundId",
                table: "EasyAbpEShopPaymentsRefundItems",
                column: "RefundId",
                principalTable: "EasyAbpEShopPaymentsRefunds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopPaymentsRefundItems_EasyAbpEShopPaymentsRefunds_RefundId",
                table: "EasyAbpEShopPaymentsRefundItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsRefundItems",
                table: "EasyAbpEShopPaymentsRefundItems");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPaymentsRefundItems",
                newName: "RefundItem");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopPaymentsRefundItems_RefundId",
                table: "RefundItem",
                newName: "IX_RefundItem_RefundId");

            migrationBuilder.AlterColumn<decimal>(
                name: "RefundAmount",
                table: "RefundItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "RefundItem",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefundItem",
                table: "RefundItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefundItem_EasyAbpEShopPaymentsRefunds_RefundId",
                table: "RefundItem",
                column: "RefundId",
                principalTable: "EasyAbpEShopPaymentsRefunds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
