using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class ChangedToSerializedEntityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedDto",
                table: "EShopProductsProductHistories");

            migrationBuilder.DropColumn(
                name: "SerializedDto",
                table: "EShopProductsProductDetailHistories");

            migrationBuilder.AddColumn<string>(
                name: "SerializedEntityData",
                table: "EShopProductsProductHistories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerializedEntityData",
                table: "EShopProductsProductDetailHistories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedEntityData",
                table: "EShopProductsProductHistories");

            migrationBuilder.DropColumn(
                name: "SerializedEntityData",
                table: "EShopProductsProductDetailHistories");

            migrationBuilder.AddColumn<string>(
                name: "SerializedDto",
                table: "EShopProductsProductHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerializedDto",
                table: "EShopProductsProductDetailHistories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
