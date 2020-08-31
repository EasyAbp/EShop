using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedActualTotalPricePropertyToOrderEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ActualTotalPrice",
                table: "EasyAbpEShopOrdersOrders",
                type: "decimal(20,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualTotalPrice",
                table: "EasyAbpEShopOrdersOrderLines",
                type: "decimal(20,8)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualTotalPrice",
                table: "EasyAbpEShopOrdersOrders");

            migrationBuilder.DropColumn(
                name: "ActualTotalPrice",
                table: "EasyAbpEShopOrdersOrderLines");
        }
    }
}
