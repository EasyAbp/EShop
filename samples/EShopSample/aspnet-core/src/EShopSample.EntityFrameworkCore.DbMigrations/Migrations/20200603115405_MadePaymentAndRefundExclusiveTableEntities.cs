using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class MadePaymentAndRefundExclusiveTableEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PaymentServiceRefunds");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "PaymentServicePayments");

            migrationBuilder.CreateTable(
                name: "EShopPaymentsPayments",
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
                    UserId = table.Column<Guid>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    PayeeAccount = table.Column<string>(nullable: true),
                    ExternalTradingCode = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    OriginalPaymentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentDiscount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ActualPaymentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CancelledTime = table.Column<DateTime>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopPaymentsPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EShopPaymentsRefunds",
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
                    PaymentId = table.Column<Guid>(nullable: false),
                    PaymentItemId = table.Column<Guid>(nullable: false),
                    RefundPaymentMethod = table.Column<string>(nullable: true),
                    ExternalTradingCode = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CustomerRemark = table.Column<string>(nullable: true),
                    StaffRemark = table.Column<string>(nullable: true),
                    StoreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopPaymentsRefunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EShopPaymentsPaymentItems",
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
                    ItemType = table.Column<string>(nullable: true),
                    ItemKey = table.Column<Guid>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    OriginalPaymentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentDiscount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ActualPaymentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EShopPaymentsPaymentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EShopPaymentsPaymentItems_EShopPaymentsPayments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "EShopPaymentsPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EShopPaymentsPaymentItems_PaymentId",
                table: "EShopPaymentsPaymentItems",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EShopPaymentsPaymentItems");

            migrationBuilder.DropTable(
                name: "EShopPaymentsRefunds");

            migrationBuilder.DropTable(
                name: "EShopPaymentsPayments");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "PaymentServiceRefunds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "PaymentServicePayments",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
