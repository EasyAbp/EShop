using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class MadeAggregateRootsMultiTenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductViews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopProductsProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductInventories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductDetailHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "EasyAbpEShopPluginsBasketsProductUpdates",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductViews");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductInventories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductHistories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopProductsProductDetailHistories");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "EasyAbpEShopPluginsBasketsProductUpdates");
        }
    }
}
