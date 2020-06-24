using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class UpgradedPaymentServiceModuleTo0_3_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonceStr",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "ReqInfo",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "NonceStr",
                table: "PaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "Sign",
                table: "PaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "SignType",
                table: "PaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.AlterColumn<int>(
                name: "SettlementRefundFee",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CashFee",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CashFeeType",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CashRefundFee",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CouponIds",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponRefundCount",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponRefundFee",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CouponRefundFees",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CouponTypes",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeeType",
                table: "PaymentServiceWeChatPayRefundRecords",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledTime",
                table: "PaymentServiceRefunds",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedTime",
                table: "PaymentServiceRefunds",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingRefundAmount",
                table: "PaymentServicePayments",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PendingRefundAmount",
                table: "PaymentServicePaymentItems",
                type: "decimal(18,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentServiceWeChatPayRefundRecords_PaymentId",
                table: "PaymentServiceWeChatPayRefundRecords",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentServiceWeChatPayPaymentRecords_PaymentId",
                table: "PaymentServiceWeChatPayPaymentRecords",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentServiceWeChatPayRefundRecords_PaymentId",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropIndex(
                name: "IX_PaymentServiceWeChatPayPaymentRecords_PaymentId",
                table: "PaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropColumn(
                name: "CashFee",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CashFeeType",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CashRefundFee",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CouponIds",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CouponRefundCount",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CouponRefundFee",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CouponRefundFees",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CouponTypes",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "FeeType",
                table: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CancelledTime",
                table: "PaymentServiceRefunds");

            migrationBuilder.DropColumn(
                name: "CompletedTime",
                table: "PaymentServiceRefunds");

            migrationBuilder.DropColumn(
                name: "PendingRefundAmount",
                table: "PaymentServicePayments");

            migrationBuilder.DropColumn(
                name: "PendingRefundAmount",
                table: "PaymentServicePaymentItems");

            migrationBuilder.AlterColumn<int>(
                name: "SettlementRefundFee",
                table: "PaymentServiceWeChatPayRefundRecords",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonceStr",
                table: "PaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReqInfo",
                table: "PaymentServiceWeChatPayRefundRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonceStr",
                table: "PaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sign",
                table: "PaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignType",
                table: "PaymentServiceWeChatPayPaymentRecords",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
