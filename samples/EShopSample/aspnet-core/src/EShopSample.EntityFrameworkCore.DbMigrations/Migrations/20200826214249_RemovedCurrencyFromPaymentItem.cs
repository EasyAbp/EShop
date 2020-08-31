using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class RemovedCurrencyFromPaymentItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "EasyAbpEShopPaymentsPaymentItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "EasyAbpEShopPaymentsPaymentItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
