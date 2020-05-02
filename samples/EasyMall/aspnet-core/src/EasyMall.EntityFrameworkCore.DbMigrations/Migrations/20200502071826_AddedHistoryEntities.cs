using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class AddedHistoryEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsProductDetailHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    ProductDetailId = table.Column<Guid>(nullable: false),
                    ModificationTime = table.Column<DateTime>(nullable: false),
                    SerializedDto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductDetailHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsProductHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    ModificationTime = table.Column<DateTime>(nullable: false),
                    SerializedDto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductHistories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsProductDetailHistories");

            migrationBuilder.DropTable(
                name: "ProductsProductHistories");
        }
    }
}
