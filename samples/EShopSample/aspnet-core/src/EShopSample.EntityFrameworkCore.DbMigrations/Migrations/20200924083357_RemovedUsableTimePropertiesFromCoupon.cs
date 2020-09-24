using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RemovedUsableTimePropertiesFromCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsableBeginTime",
                table: "EasyAbpEShopPluginsCouponsCoupons");

            migrationBuilder.DropColumn(
                name: "UsableEndTime",
                table: "EasyAbpEShopPluginsCouponsCoupons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UsableBeginTime",
                table: "EasyAbpEShopPluginsCouponsCoupons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsableEndTime",
                table: "EasyAbpEShopPluginsCouponsCoupons",
                type: "datetime2",
                nullable: true);
        }
    }
}
