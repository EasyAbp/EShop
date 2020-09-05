using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedExtraPropertiesToSomeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EasyAbpEShopProductsProductAttributes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EasyAbpEShopProductsProductAttributeOptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "EasyAbpEShopOrdersOrderLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EasyAbpEShopProductsProductAttributes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EasyAbpEShopProductsProductAttributeOptions");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "EasyAbpEShopOrdersOrderLines");
        }
    }
}
