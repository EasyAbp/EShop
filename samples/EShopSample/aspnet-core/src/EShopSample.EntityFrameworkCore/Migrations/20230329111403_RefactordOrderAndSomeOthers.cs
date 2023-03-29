using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    /// <inheritdoc />
    public partial class RefactordOrderAndSomeOthers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "PaymentExpireIn",
                table: "EasyAbpEShopProductsProductViews",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "EasyAbpEShopPaymentsRefundItemOrderExtraFees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "EasyAbpEShopOrdersOrderExtraFees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopOrdersOrderDiscounts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderLineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountedAmount = table.Column<decimal>(type: "decimal(20,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopOrdersOrderDiscounts", x => new { x.OrderId, x.OrderLineId, x.Name, x.Key });
                    table.ForeignKey(
                        name: "FK_EasyAbpEShopOrdersOrderDiscounts_EasyAbpEShopOrdersOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "EasyAbpEShopOrdersOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpEShopOrdersOrderDiscounts");

            migrationBuilder.DropColumn(
                name: "PaymentExpireIn",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "EasyAbpEShopPaymentsRefundItemOrderExtraFees");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "EasyAbpEShopOrdersOrderExtraFees");
        }
    }
}
