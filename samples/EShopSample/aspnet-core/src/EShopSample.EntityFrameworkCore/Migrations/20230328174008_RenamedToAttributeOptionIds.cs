using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShopSample.Migrations
{
    /// <inheritdoc />
    public partial class RenamedToAttributeOptionIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SerializedAttributeOptionIds",
                table: "EasyAbpEShopProductsProductSkus",
                newName: "AttributeOptionIds");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttributeOptionIds",
                table: "EasyAbpEShopProductsProductSkus",
                newName: "SerializedAttributeOptionIds");
        }
    }
}
