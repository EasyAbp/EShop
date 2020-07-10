using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedNameAndRemovedParentCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "EShopProductsCategories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EShopProductsCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EShopProductsCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "EShopProductsCategories",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
