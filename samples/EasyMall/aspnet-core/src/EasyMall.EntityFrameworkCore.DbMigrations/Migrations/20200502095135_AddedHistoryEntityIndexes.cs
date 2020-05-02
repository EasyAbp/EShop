using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class AddedHistoryEntityIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductHistories_ModificationTime",
                table: "ProductsProductHistories",
                column: "ModificationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductHistories_ProductId",
                table: "ProductsProductHistories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductDetailHistories_ModificationTime",
                table: "ProductsProductDetailHistories",
                column: "ModificationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductDetailHistories_ProductDetailId",
                table: "ProductsProductDetailHistories",
                column: "ProductDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductsProductHistories_ModificationTime",
                table: "ProductsProductHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductsProductHistories_ProductId",
                table: "ProductsProductHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductsProductDetailHistories_ModificationTime",
                table: "ProductsProductDetailHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProductsProductDetailHistories_ProductDetailId",
                table: "ProductsProductDetailHistories");
        }
    }
}
