using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class AddedPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentsPayments",
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
                    PaymentMethod = table.Column<string>(nullable: true),
                    ExternalTradingCode = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    OriginalPaymentAmount = table.Column<decimal>(nullable: false),
                    PaymentDiscount = table.Column<decimal>(nullable: false),
                    ActualPaymentAmount = table.Column<decimal>(nullable: false),
                    RefundAmount = table.Column<decimal>(nullable: false),
                    CompletionTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentsRefunds",
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
                    OrderId = table.Column<Guid>(nullable: false),
                    RefundPaymentMethod = table.Column<string>(nullable: true),
                    ExternalTradingCode = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    RefundAmount = table.Column<decimal>(nullable: false),
                    CustomerRemark = table.Column<string>(nullable: true),
                    StaffRemark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsRefunds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentsPaymentItems",
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
                    OriginalPaymentAmount = table.Column<decimal>(nullable: false),
                    PaymentDiscount = table.Column<decimal>(nullable: false),
                    ActualPaymentAmount = table.Column<decimal>(nullable: false),
                    RefundAmount = table.Column<decimal>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentsPaymentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentsPaymentItems_PaymentsPayments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentsPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentsPaymentItems_PaymentId",
                table: "PaymentsPaymentItems",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentsPaymentItems");

            migrationBuilder.DropTable(
                name: "PaymentsRefunds");

            migrationBuilder.DropTable(
                name: "PaymentsPayments");
        }
    }
}
