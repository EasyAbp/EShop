using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedCouponsModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPluginsCouponsCoupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    CouponTemplateId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    UsableBeginTime = table.Column<DateTime>(nullable: true),
                    UsableEndTime = table.Column<DateTime>(nullable: true),
                    UsedTime = table.Column<DateTime>(nullable: true),
                    DiscountedAmount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPluginsCouponsCoupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpEShopPluginsCouponsCouponTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: true),
                    CouponType = table.Column<int>(nullable: false),
                    UniqueName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UsableDuration = table.Column<TimeSpan>(nullable: true),
                    UsableBeginTime = table.Column<DateTime>(nullable: true),
                    UsableEndTime = table.Column<DateTime>(nullable: true),
                    ConditionAmount = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    IsCrossProductAllowed = table.Column<bool>(nullable: false),
                    IsUnscoped = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopPluginsCouponsCouponTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CouponTemplateScope",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    ProductSkuId = table.Column<Guid>(nullable: true),
                    CouponTemplateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponTemplateScope", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponTemplateScope_EasyAbpEShopPluginsCouponsCouponTemplates_CouponTemplateId",
                        column: x => x.CouponTemplateId,
                        principalTable: "EasyAbpEShopPluginsCouponsCouponTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CouponTemplateScope_CouponTemplateId",
                table: "CouponTemplateScope",
                column: "CouponTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponTemplateScope");

            migrationBuilder.DropTable(
                name: "EasyAbpEShopPluginsCouponsCoupons");

            migrationBuilder.DropTable(
                name: "EasyAbpEShopPluginsCouponsCouponTemplates");
        }
    }
}
