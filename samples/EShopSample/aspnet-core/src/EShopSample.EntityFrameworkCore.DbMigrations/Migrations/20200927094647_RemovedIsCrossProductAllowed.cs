using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RemovedIsCrossProductAllowed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCrossProductAllowed",
                table: "EasyAbpEShopPluginsCouponsCouponTemplates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCrossProductAllowed",
                table: "EasyAbpEShopPluginsCouponsCouponTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
