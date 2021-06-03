using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RemovedUniqueIndexOfCategoryUniqueName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EasyAbpEShopProductsCategories_UniqueName",
                table: "EasyAbpEShopProductsCategories");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueName",
                table: "EasyAbpEShopProductsCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueName",
                table: "EasyAbpEShopProductsCategories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpEShopProductsCategories_UniqueName",
                table: "EasyAbpEShopProductsCategories",
                column: "UniqueName",
                unique: true,
                filter: "[UniqueName] IS NOT NULL");
        }
    }
}
