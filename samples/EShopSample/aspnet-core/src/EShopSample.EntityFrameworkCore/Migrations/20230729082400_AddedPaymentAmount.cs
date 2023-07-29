using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    /// <inheritdoc />
    public partial class AddedPaymentAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "EasyAbpEShopOrdersOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "EasyAbpEShopOrdersOrderLines",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "EasyAbpEShopOrdersOrderExtraFees",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "EasyAbpEShopOrdersOrders");

            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "EasyAbpEShopOrdersOrderExtraFees");
        }
    }
}
