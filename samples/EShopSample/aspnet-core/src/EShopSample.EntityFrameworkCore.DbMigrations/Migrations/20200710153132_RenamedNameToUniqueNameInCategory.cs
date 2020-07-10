using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RenamedNameToUniqueNameInCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EShopProductsCategories");

            migrationBuilder.AddColumn<string>(
                name: "UniqueName",
                table: "EShopProductsCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsCategories_UniqueName",
                table: "EShopProductsCategories",
                column: "UniqueName",
                unique: true,
                filter: "[UniqueName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EShopProductsCategories_UniqueName",
                table: "EShopProductsCategories");

            migrationBuilder.DropColumn(
                name: "UniqueName",
                table: "EShopProductsCategories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EShopProductsCategories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
