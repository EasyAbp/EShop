using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedPrepaymentModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpPaymentServicePrepaymentAccounts",
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
                    AccountGroupName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    LockedBalance = table.Column<decimal>(type: "decimal(20,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpPaymentServicePrepaymentAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpPaymentServicePrepaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    AccountId = table.Column<Guid>(nullable: false),
                    AccountUserId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: true),
                    TransactionType = table.Column<int>(nullable: false),
                    ActionName = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<string>(nullable: true),
                    ExternalTradingCode = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    ChangedBalance = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    OriginalBalance = table.Column<decimal>(type: "decimal(20,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpPaymentServicePrepaymentTransactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpPaymentServicePrepaymentAccounts_UserId",
                table: "EasyAbpPaymentServicePrepaymentAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpPaymentServicePrepaymentTransactions_AccountId",
                table: "EasyAbpPaymentServicePrepaymentTransactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpPaymentServicePrepaymentTransactions_AccountUserId",
                table: "EasyAbpPaymentServicePrepaymentTransactions",
                column: "AccountUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpPaymentServicePrepaymentAccounts");

            migrationBuilder.DropTable(
                name: "EasyAbpPaymentServicePrepaymentTransactions");
        }
    }
}
