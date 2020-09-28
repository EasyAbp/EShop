using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class ConfiguredCouponTemplateScope : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CouponTemplateScope_EasyAbpEShopPluginsCouponsCouponTemplates_CouponTemplateId",
                table: "CouponTemplateScope");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CouponTemplateScope",
                table: "CouponTemplateScope");

            migrationBuilder.RenameTable(
                name: "CouponTemplateScope",
                newName: "EasyAbpEShopPluginsCouponsCouponTemplateScopes");

            migrationBuilder.RenameIndex(
                name: "IX_CouponTemplateScope_CouponTemplateId",
                table: "EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                newName: "IX_EasyAbpEShopPluginsCouponsCouponTemplateScopes_CouponTemplateId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                table: "EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopPluginsCouponsCouponTemplateScopes_EasyAbpEShopPluginsCouponsCouponTemplates_CouponTemplateId",
                table: "EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                column: "CouponTemplateId",
                principalTable: "EasyAbpEShopPluginsCouponsCouponTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopPluginsCouponsCouponTemplateScopes_EasyAbpEShopPluginsCouponsCouponTemplates_CouponTemplateId",
                table: "EasyAbpEShopPluginsCouponsCouponTemplateScopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                table: "EasyAbpEShopPluginsCouponsCouponTemplateScopes");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPluginsCouponsCouponTemplateScopes",
                newName: "CouponTemplateScope");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopPluginsCouponsCouponTemplateScopes_CouponTemplateId",
                table: "CouponTemplateScope",
                newName: "IX_CouponTemplateScope_CouponTemplateId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "CouponTemplateScope",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CouponTemplateScope",
                table: "CouponTemplateScope",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CouponTemplateScope_EasyAbpEShopPluginsCouponsCouponTemplates_CouponTemplateId",
                table: "CouponTemplateScope",
                column: "CouponTemplateId",
                principalTable: "EasyAbpEShopPluginsCouponsCouponTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
