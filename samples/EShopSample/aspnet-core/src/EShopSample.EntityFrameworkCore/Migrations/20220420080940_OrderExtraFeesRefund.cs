using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    public partial class OrderExtraFeesRefund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrderExtraFees",
                type: "decimal(20,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPaymentsRefundItemOrderExtraFees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    RefundItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPaymentsRefundItemOrderExtraFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EasyAbpEShopPaymentsRefundItemOrderExtraFees_EasyAbpEShopPaymentsRefundItems_RefundItemId",
                        column: x => x.RefundItemId,
                        principalTable: "EasyAbpEShopPaymentsRefundItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopPaymentsRefundItemOrderExtraFees_RefundItemId",
                table: "EasyAbpEShopPaymentsRefundItemOrderExtraFees",
                column: "RefundItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpEShopPaymentsRefundItemOrderExtraFees");

            migrationBuilder.DropColumn(
                name: "RefundAmount",
                table: "EasyAbpEShopOrdersOrderExtraFees");
        }
    }
}
