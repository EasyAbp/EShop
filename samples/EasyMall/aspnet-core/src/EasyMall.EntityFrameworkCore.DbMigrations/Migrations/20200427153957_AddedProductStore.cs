using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class AddedProductStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "ProductsProducts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProductsProducts");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "ProductsProductSkus",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "ProductsProductCategories",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProductsProductStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    IsOwner = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductStores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsProductStores");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "ProductsProductSkus");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "ProductsProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "ProductsProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StoreId",
                table: "ProductsProductCategories",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
