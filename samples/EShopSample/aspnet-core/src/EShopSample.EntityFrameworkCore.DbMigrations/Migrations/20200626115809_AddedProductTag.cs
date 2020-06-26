using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedProductTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "EShopProductsCategories");

            migrationBuilder.CreateTable(
                name: "EShopProductsProductTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    TagId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopProductsProductTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EShopProductsTags",
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
                    Description = table.Column<string>(nullable: true),
                    MediaResources = table.Column<string>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopProductsTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EShopProductsTags_EShopProductsTags_ParentId",
                        column: x => x.ParentId,
                        principalTable: "EShopProductsTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsProductTags_ProductId",
                table: "EShopProductsProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsProductTags_TagId",
                table: "EShopProductsProductTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsTags_ParentId",
                table: "EShopProductsTags",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_EShopProductsTags_StoreId",
                table: "EShopProductsTags",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EShopProductsProductTags");

            migrationBuilder.DropTable(
                name: "EShopProductsTags");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentCategoryId",
                table: "EShopProductsCategories",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
