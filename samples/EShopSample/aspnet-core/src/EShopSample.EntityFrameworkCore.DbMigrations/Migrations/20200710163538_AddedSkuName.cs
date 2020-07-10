using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedSkuName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkuName",
                table: "EShopPluginsBasketsBasketItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SkuName",
                table: "EShopOrdersOrderLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkuName",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "SkuName",
                table: "EShopOrdersOrderLines");
        }
    }
}
