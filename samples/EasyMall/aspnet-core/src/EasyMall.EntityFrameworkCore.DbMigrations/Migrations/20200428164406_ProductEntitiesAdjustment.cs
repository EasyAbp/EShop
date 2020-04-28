using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class ProductEntitiesAdjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsProductDetails_ProductsProducts_ProductId",
                table: "ProductsProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductDetails",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductsProductDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductDetailId",
                table: "ProductsProductSkus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductDetailId",
                table: "ProductsProducts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProductsProductDetails",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ProductsProductDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductsProductDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "ProductsProductDetails",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductDetails",
                table: "ProductsProductDetails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductDetails",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "ProductsProductSkus");

            migrationBuilder.DropColumn(
                name: "ProductDetailId",
                table: "ProductsProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ProductsProductDetails");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "ProductsProductDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductsProductDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductDetails",
                table: "ProductsProductDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsProductDetails_ProductsProducts_ProductId",
                table: "ProductsProductDetails",
                column: "ProductId",
                principalTable: "ProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
