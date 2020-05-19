using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyMall.Migrations
{
    public partial class RenamedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersOrderLines_OrdersOrders_OrderId",
                table: "OrdersOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsProductAttributeOptions_ProductsProductAttributes_ProductAttributeId",
                table: "ProductsProductAttributeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsProductAttributes_ProductsProducts_ProductId",
                table: "ProductsProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsProductSkus_ProductsProducts_ProductId",
                table: "ProductsProductSkus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoresStores",
                table: "StoresStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductTypes",
                table: "ProductsProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductStores",
                table: "ProductsProductStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductSkus",
                table: "ProductsProductSkus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProducts",
                table: "ProductsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductHistories",
                table: "ProductsProductHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductDetails",
                table: "ProductsProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductDetailHistories",
                table: "ProductsProductDetailHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductCategories",
                table: "ProductsProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductAttributes",
                table: "ProductsProductAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsProductAttributeOptions",
                table: "ProductsProductAttributeOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsCategories",
                table: "ProductsCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersOrders",
                table: "OrdersOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersOrderLines",
                table: "OrdersOrderLines");

            migrationBuilder.RenameTable(
                name: "StoresStores",
                newName: "EShopStoresStores");

            migrationBuilder.RenameTable(
                name: "ProductsProductTypes",
                newName: "EShopProductsProductTypes");

            migrationBuilder.RenameTable(
                name: "ProductsProductStores",
                newName: "EShopProductsProductStores");

            migrationBuilder.RenameTable(
                name: "ProductsProductSkus",
                newName: "EShopProductsProductSkus");

            migrationBuilder.RenameTable(
                name: "ProductsProducts",
                newName: "EShopProductsProducts");

            migrationBuilder.RenameTable(
                name: "ProductsProductHistories",
                newName: "EShopProductsProductHistories");

            migrationBuilder.RenameTable(
                name: "ProductsProductDetails",
                newName: "EShopProductsProductDetails");

            migrationBuilder.RenameTable(
                name: "ProductsProductDetailHistories",
                newName: "EShopProductsProductDetailHistories");

            migrationBuilder.RenameTable(
                name: "ProductsProductCategories",
                newName: "EShopProductsProductCategories");

            migrationBuilder.RenameTable(
                name: "ProductsProductAttributes",
                newName: "EShopProductsProductAttributes");

            migrationBuilder.RenameTable(
                name: "ProductsProductAttributeOptions",
                newName: "EShopProductsProductAttributeOptions");

            migrationBuilder.RenameTable(
                name: "ProductsCategories",
                newName: "EShopProductsCategories");

            migrationBuilder.RenameTable(
                name: "OrdersOrders",
                newName: "EShopOrdersOrders");

            migrationBuilder.RenameTable(
                name: "OrdersOrderLines",
                newName: "EShopOrdersOrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductSkus_ProductId",
                table: "EShopProductsProductSkus",
                newName: "IX_EShopProductsProductSkus_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductHistories_ProductId",
                table: "EShopProductsProductHistories",
                newName: "IX_EShopProductsProductHistories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductHistories_ModificationTime",
                table: "EShopProductsProductHistories",
                newName: "IX_EShopProductsProductHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductDetailHistories_ProductDetailId",
                table: "EShopProductsProductDetailHistories",
                newName: "IX_EShopProductsProductDetailHistories_ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductDetailHistories_ModificationTime",
                table: "EShopProductsProductDetailHistories",
                newName: "IX_EShopProductsProductDetailHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductAttributes_ProductId",
                table: "EShopProductsProductAttributes",
                newName: "IX_EShopProductsProductAttributes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductsProductAttributeOptions_ProductAttributeId",
                table: "EShopProductsProductAttributeOptions",
                newName: "IX_EShopProductsProductAttributeOptions_ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersOrderLines_OrderId",
                table: "EShopOrdersOrderLines",
                newName: "IX_EShopOrdersOrderLines_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopStoresStores",
                table: "EShopStoresStores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductTypes",
                table: "EShopProductsProductTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductStores",
                table: "EShopProductsProductStores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductSkus",
                table: "EShopProductsProductSkus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProducts",
                table: "EShopProductsProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductHistories",
                table: "EShopProductsProductHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductDetails",
                table: "EShopProductsProductDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductDetailHistories",
                table: "EShopProductsProductDetailHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductCategories",
                table: "EShopProductsProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductAttributes",
                table: "EShopProductsProductAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsProductAttributeOptions",
                table: "EShopProductsProductAttributeOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopProductsCategories",
                table: "EShopProductsCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopOrdersOrders",
                table: "EShopOrdersOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopOrdersOrderLines",
                table: "EShopOrdersOrderLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EShopOrdersOrderLines_EShopOrdersOrders_OrderId",
                table: "EShopOrdersOrderLines",
                column: "OrderId",
                principalTable: "EShopOrdersOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EShopProductsProductAttributeOptions_EShopProductsProductAttributes_ProductAttributeId",
                table: "EShopProductsProductAttributeOptions",
                column: "ProductAttributeId",
                principalTable: "EShopProductsProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EShopProductsProductAttributes_EShopProductsProducts_ProductId",
                table: "EShopProductsProductAttributes",
                column: "ProductId",
                principalTable: "EShopProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EShopProductsProductSkus_EShopProductsProducts_ProductId",
                table: "EShopProductsProductSkus",
                column: "ProductId",
                principalTable: "EShopProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EShopOrdersOrderLines_EShopOrdersOrders_OrderId",
                table: "EShopOrdersOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_EShopProductsProductAttributeOptions_EShopProductsProductAttributes_ProductAttributeId",
                table: "EShopProductsProductAttributeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_EShopProductsProductAttributes_EShopProductsProducts_ProductId",
                table: "EShopProductsProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_EShopProductsProductSkus_EShopProductsProducts_ProductId",
                table: "EShopProductsProductSkus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopStoresStores",
                table: "EShopStoresStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductTypes",
                table: "EShopProductsProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductStores",
                table: "EShopProductsProductStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductSkus",
                table: "EShopProductsProductSkus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProducts",
                table: "EShopProductsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductHistories",
                table: "EShopProductsProductHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductDetails",
                table: "EShopProductsProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductDetailHistories",
                table: "EShopProductsProductDetailHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductCategories",
                table: "EShopProductsProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductAttributes",
                table: "EShopProductsProductAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsProductAttributeOptions",
                table: "EShopProductsProductAttributeOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopProductsCategories",
                table: "EShopProductsCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopOrdersOrders",
                table: "EShopOrdersOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopOrdersOrderLines",
                table: "EShopOrdersOrderLines");

            migrationBuilder.RenameTable(
                name: "EShopStoresStores",
                newName: "StoresStores");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductTypes",
                newName: "ProductsProductTypes");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductStores",
                newName: "ProductsProductStores");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductSkus",
                newName: "ProductsProductSkus");

            migrationBuilder.RenameTable(
                name: "EShopProductsProducts",
                newName: "ProductsProducts");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductHistories",
                newName: "ProductsProductHistories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductDetails",
                newName: "ProductsProductDetails");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductDetailHistories",
                newName: "ProductsProductDetailHistories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductCategories",
                newName: "ProductsProductCategories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductAttributes",
                newName: "ProductsProductAttributes");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductAttributeOptions",
                newName: "ProductsProductAttributeOptions");

            migrationBuilder.RenameTable(
                name: "EShopProductsCategories",
                newName: "ProductsCategories");

            migrationBuilder.RenameTable(
                name: "EShopOrdersOrders",
                newName: "OrdersOrders");

            migrationBuilder.RenameTable(
                name: "EShopOrdersOrderLines",
                newName: "OrdersOrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductSkus_ProductId",
                table: "ProductsProductSkus",
                newName: "IX_ProductsProductSkus_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductHistories_ProductId",
                table: "ProductsProductHistories",
                newName: "IX_ProductsProductHistories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductHistories_ModificationTime",
                table: "ProductsProductHistories",
                newName: "IX_ProductsProductHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductDetailHistories_ProductDetailId",
                table: "ProductsProductDetailHistories",
                newName: "IX_ProductsProductDetailHistories_ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductDetailHistories_ModificationTime",
                table: "ProductsProductDetailHistories",
                newName: "IX_ProductsProductDetailHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductAttributes_ProductId",
                table: "ProductsProductAttributes",
                newName: "IX_ProductsProductAttributes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductAttributeOptions_ProductAttributeId",
                table: "ProductsProductAttributeOptions",
                newName: "IX_ProductsProductAttributeOptions_ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopOrdersOrderLines_OrderId",
                table: "OrdersOrderLines",
                newName: "IX_OrdersOrderLines_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoresStores",
                table: "StoresStores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductTypes",
                table: "ProductsProductTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductStores",
                table: "ProductsProductStores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductSkus",
                table: "ProductsProductSkus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProducts",
                table: "ProductsProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductHistories",
                table: "ProductsProductHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductDetails",
                table: "ProductsProductDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductDetailHistories",
                table: "ProductsProductDetailHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductCategories",
                table: "ProductsProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductAttributes",
                table: "ProductsProductAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsProductAttributeOptions",
                table: "ProductsProductAttributeOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsCategories",
                table: "ProductsCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersOrders",
                table: "OrdersOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersOrderLines",
                table: "OrdersOrderLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersOrderLines_OrdersOrders_OrderId",
                table: "OrdersOrderLines",
                column: "OrderId",
                principalTable: "OrdersOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsProductAttributeOptions_ProductsProductAttributes_ProductAttributeId",
                table: "ProductsProductAttributeOptions",
                column: "ProductAttributeId",
                principalTable: "ProductsProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsProductAttributes_ProductsProducts_ProductId",
                table: "ProductsProductAttributes",
                column: "ProductId",
                principalTable: "ProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsProductSkus_ProductsProducts_ProductId",
                table: "ProductsProductSkus",
                column: "ProductId",
                principalTable: "ProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
