using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedOrderMaxQuantityProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderMaxQuantity",
                table: "EShopProductsProductSkus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderMaxQuantity",
                table: "EShopProductsProductSkus");
        }
    }
}
