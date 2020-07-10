using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class MadeUniqueNameUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueName",
                table: "EShopProductsProducts",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsProducts_UniqueName",
                table: "EShopProductsProducts",
                column: "UniqueName",
                unique: true,
                filter: "[UniqueName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EShopProductsProducts_UniqueName",
                table: "EShopProductsProducts");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueName",
                table: "EShopProductsProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
