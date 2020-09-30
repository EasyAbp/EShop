using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedCurrencyToCouponAndCouponTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPluginsCouponsCouponTemplates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPluginsCouponsCoupons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPluginsCouponsCouponTemplates");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPluginsCouponsCoupons");
        }
    }
}
