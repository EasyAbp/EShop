using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class RemovedStoreIdFromProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ProductsProductCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "ProductsProductCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
