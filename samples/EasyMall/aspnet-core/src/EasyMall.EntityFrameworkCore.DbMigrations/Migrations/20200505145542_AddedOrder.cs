using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class AddedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersOrders",
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
                    CustomerUserId = table.Column<Guid>(nullable: false),
                    OrderStatus = table.Column<int>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    ProductTotalPrice = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    RefundedAmount = table.Column<decimal>(nullable: false),
                    CustomerRemark = table.Column<string>(nullable: true),
                    StaffRemark = table.Column<string>(nullable: true),
                    PaidTime = table.Column<DateTime>(nullable: true),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    CancelledTime = table.Column<DateTime>(nullable: true),
                    ReducedInventoryAfterPlacingTime = table.Column<DateTime>(nullable: true),
                    ReducedInventoryAfterPaymentTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersOrderLines",
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
                    ProductId = table.Column<Guid>(nullable: false),
                    ProductSkuId = table.Column<Guid>(nullable: false),
                    ProductModificationTime = table.Column<DateTime>(nullable: false),
                    ProductDetailModificationTime = table.Column<DateTime>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    SkuDescription = table.Column<string>(nullable: true),
                    MediaResources = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    TotalDiscount = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersOrderLines_OrdersOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersOrderLines_OrderId",
                table: "OrdersOrderLines",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersOrderLines");

            migrationBuilder.DropTable(
                name: "OrdersOrders");
        }
    }
}
