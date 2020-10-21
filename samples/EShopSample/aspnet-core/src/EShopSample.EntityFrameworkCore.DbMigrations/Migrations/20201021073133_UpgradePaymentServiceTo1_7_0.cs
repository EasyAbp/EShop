using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class UpgradePaymentServiceTo1_7_0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpPaymentServicePrepaymentWithdrawalRecords",
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
                    AccountId = table.Column<Guid>(nullable: false),
                    WithdrawalMethod = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CancellationTime = table.Column<DateTime>(nullable: true),
                    ResultErrorCode = table.Column<string>(nullable: true),
                    ResultErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpPaymentServicePrepaymentWithdrawalRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyAbpPaymentServicePrepaymentWithdrawalRequests",
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
                    AccountId = table.Column<Guid>(nullable: false),
                    AccountUserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20,8)", nullable: false),
                    ReviewTime = table.Column<DateTime>(nullable: true),
                    ReviewerUserId = table.Column<Guid>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpPaymentServicePrepaymentWithdrawalRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpPaymentServicePrepaymentWithdrawalRecords");

            migrationBuilder.DropTable(
                name: "EasyAbpPaymentServicePrepaymentWithdrawalRequests");
        }
    }
}
