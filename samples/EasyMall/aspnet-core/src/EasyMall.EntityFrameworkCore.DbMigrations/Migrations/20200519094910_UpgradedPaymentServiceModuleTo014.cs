using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class UpgradedPaymentServiceModuleTo014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledTime",
                table: "PaymentServicePayments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentServiceWeChatPayPaymentRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: false),
                    ReturnCode = table.Column<string>(nullable: true),
                    ReturnMsg = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    MchId = table.Column<string>(nullable: true),
                    DeviceInfo = table.Column<string>(nullable: true),
                    NonceStr = table.Column<string>(nullable: true),
                    Sign = table.Column<string>(nullable: true),
                    SignType = table.Column<string>(nullable: true),
                    ResultCode = table.Column<string>(nullable: true),
                    ErrCode = table.Column<string>(nullable: true),
                    ErrCodeDes = table.Column<string>(nullable: true),
                    Openid = table.Column<string>(nullable: true),
                    IsSubscribe = table.Column<string>(nullable: true),
                    TradeType = table.Column<string>(nullable: true),
                    BankType = table.Column<string>(nullable: true),
                    TotalFee = table.Column<int>(nullable: false),
                    SettlementTotalFee = table.Column<int>(nullable: true),
                    FeeType = table.Column<string>(nullable: true),
                    CashFee = table.Column<int>(nullable: false),
                    CashFeeType = table.Column<string>(nullable: true),
                    CouponFee = table.Column<int>(nullable: true),
                    CouponCount = table.Column<int>(nullable: true),
                    CouponTypes = table.Column<string>(nullable: true),
                    CouponIds = table.Column<string>(nullable: true),
                    CouponFees = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    OutTradeNo = table.Column<string>(nullable: true),
                    Attach = table.Column<string>(nullable: true),
                    TimeEnd = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentServiceWeChatPayPaymentRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentServiceWeChatPayRefundRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    PaymentId = table.Column<Guid>(nullable: false),
                    ReturnCode = table.Column<string>(nullable: true),
                    ReturnMsg = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    MchId = table.Column<string>(nullable: true),
                    NonceStr = table.Column<string>(nullable: true),
                    ReqInfo = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true),
                    OutTradeNo = table.Column<string>(nullable: true),
                    RefundId = table.Column<string>(nullable: true),
                    OutRefundNo = table.Column<string>(nullable: true),
                    TotalFee = table.Column<int>(nullable: false),
                    SettlementTotalFee = table.Column<int>(nullable: true),
                    RefundFee = table.Column<int>(nullable: false),
                    SettlementRefundFee = table.Column<int>(nullable: false),
                    RefundStatus = table.Column<string>(nullable: true),
                    SuccessTime = table.Column<string>(nullable: true),
                    RefundRecvAccout = table.Column<string>(nullable: true),
                    RefundAccount = table.Column<string>(nullable: true),
                    RefundRequestSource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentServiceWeChatPayRefundRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentServiceWeChatPayPaymentRecords");

            migrationBuilder.DropTable(
                name: "PaymentServiceWeChatPayRefundRecords");

            migrationBuilder.DropColumn(
                name: "CancelledTime",
                table: "PaymentServicePayments");
        }
    }
}
