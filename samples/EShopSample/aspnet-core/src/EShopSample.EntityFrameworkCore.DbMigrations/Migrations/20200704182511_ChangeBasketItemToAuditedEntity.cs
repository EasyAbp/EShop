using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class ChangeBasketItemToAuditedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "EShopPluginsBasketsBasketItems",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "EShopPluginsBasketsBasketItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "EShopPluginsBasketsBasketItems");
        }
    }
}
