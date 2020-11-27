using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedOrderExtraFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpEShopOrdersOrderExtraFees",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Key = table.Column<string>(nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(20,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpEShopOrdersOrderExtraFees", x => new { x.OrderId, x.Name, x.Key });
                    table.ForeignKey(
                        name: "FK_EasyAbpEShopOrdersOrderExtraFees_EasyAbpEShopOrdersOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "EasyAbpEShopOrdersOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpEShopOrdersOrderExtraFees");
        }
    }
}
