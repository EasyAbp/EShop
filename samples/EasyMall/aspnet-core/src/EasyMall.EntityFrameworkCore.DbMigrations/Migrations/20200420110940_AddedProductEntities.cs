using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class AddedProductEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsProductAttributes_ProductsProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductsProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsProductDetails",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductDetails", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductsProductDetails_ProductsProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductsProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsProductSkus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    SerializedAttributeOptionIds = table.Column<string>(nullable: true),
                    OriginalPrice = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Inventory = table.Column<int>(nullable: false),
                    Sold = table.Column<int>(nullable: false),
                    OrderMinQuantity = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductSkus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsProductSkus_ProductsProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductsProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsProductAttributeOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProductAttributeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProductAttributeOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsProductAttributeOptions_ProductsProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductsProductAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductAttributeOptions_ProductAttributeId",
                table: "ProductsProductAttributeOptions",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductAttributes_ProductId",
                table: "ProductsProductAttributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProductSkus_ProductId",
                table: "ProductsProductSkus",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsProductAttributeOptions");

            migrationBuilder.DropTable(
                name: "ProductsProductDetails");

            migrationBuilder.DropTable(
                name: "ProductsProductSkus");

            migrationBuilder.DropTable(
                name: "ProductsProductAttributes");
        }
    }
}
