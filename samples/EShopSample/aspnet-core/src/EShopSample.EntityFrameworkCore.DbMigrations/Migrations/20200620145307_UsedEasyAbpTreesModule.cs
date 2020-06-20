using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class UsedEasyAbpTreesModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "EShopProductsCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "EShopProductsCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "EShopProductsCategories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsCategories_ParentId",
                table: "EShopProductsCategories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EShopProductsCategories_EShopProductsCategories_ParentId",
                table: "EShopProductsCategories",
                column: "ParentId",
                principalTable: "EShopProductsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EShopProductsCategories_EShopProductsCategories_ParentId",
                table: "EShopProductsCategories");

            migrationBuilder.DropIndex(
                name: "IX_EShopProductsCategories_ParentId",
                table: "EShopProductsCategories");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "EShopProductsCategories");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "EShopProductsCategories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "EShopProductsCategories");
        }
    }
}
