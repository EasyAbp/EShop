using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class ConfiguredDecimalPropertiesInProductView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumPrice",
                table: "EasyAbpEShopProductsProductViews",
                type: "decimal(20,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaximumPrice",
                table: "EasyAbpEShopProductsProductViews",
                type: "decimal(20,8)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumPrice",
                table: "EasyAbpEShopProductsProductViews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaximumPrice",
                table: "EasyAbpEShopProductsProductViews",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,8)",
                oldNullable: true);
        }
    }
}
