using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedOrderNumberProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "EShopOrdersOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EShopOrdersOrders_OrderNumber",
                table: "EShopOrdersOrders",
                column: "OrderNumber",
                unique: true,
                filter: "[OrderNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EShopOrdersOrders_OrderNumber",
                table: "EShopOrdersOrders");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "EShopOrdersOrders");
        }
    }
}
