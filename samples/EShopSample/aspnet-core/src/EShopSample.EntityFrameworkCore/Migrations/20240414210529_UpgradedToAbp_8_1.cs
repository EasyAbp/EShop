using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    /// <inheritdoc />
    public partial class UpgradedToAbp_8_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateTime",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundsAccount",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionDetail",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserReceivedAccount",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Amount",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Payer",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionDetail",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SceneInfo",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuccessTime",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeState",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeStateDesc",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "AbpTenants",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Providers",
                table: "AbpSettingDefinitions",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultValue",
                table: "AbpSettingDefinitions",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants",
                column: "NormalizedName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "FundsAccount",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "PromotionDetail",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "UserReceivedAccount",
                table: "EasyAbpPaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "Payer",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "PromotionDetail",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "SceneInfo",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "SuccessTime",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "TradeState",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "TradeStateDesc",
                table: "EasyAbpPaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "AbpTenants");

            migrationBuilder.AlterColumn<string>(
                name: "Providers",
                table: "AbpSettingDefinitions",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DefaultValue",
                table: "AbpSettingDefinitions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}
