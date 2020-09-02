using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RemovedProductTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpEShopProductsProductTypes");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.DropColumn(
                name: "ProductTypeUniqueName",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.AddColumn<string>(
                name: "ProductGroupName",
                table: "EasyAbpEShopProductsProducts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductGroupDisplayName",
                table: "EasyAbpEShopOrdersOrderLines",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductGroupName",
                table: "EasyAbpEShopOrdersOrderLines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductGroupName",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.DropColumn(
                name: "ProductGroupDisplayName",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.DropColumn(
                name: "ProductGroupName",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeId",
                table: "EasyAbpEShopProductsProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ProductTypeUniqueName",
                table: "EasyAbpEShopOrdersOrderLines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopProductsProductTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MultiTenancySide = table.Column<int>(type: "int", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopProductsProductTypes", x => x.Id);
                });
        }
    }
}
