using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RenamedCodeToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                newName: "Name",
                table: "EShopProductsProductSkus");

            migrationBuilder.RenameColumn(
                name: "Code",
                newName: "UniqueName",
                table: "EShopProductsProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                newName: "Code",
                table: "EShopProductsProductSkus");

            migrationBuilder.RenameColumn(
                name: "UniqueName",
                newName: "Code",
                table: "EShopProductsProducts");
        }
    }
}
