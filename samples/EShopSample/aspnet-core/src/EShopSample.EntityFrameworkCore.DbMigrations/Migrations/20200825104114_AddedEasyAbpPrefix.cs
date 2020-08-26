using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSample.Migrations
{
    public partial class AddedEasyAbpPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EShopOrdersOrderLines_EShopOrdersOrders_OrderId",
                table: "EShopOrdersOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_EShopPaymentsPaymentItems_EShopPaymentsPayments_PaymentId",
                table: "EShopPaymentsPaymentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EShopProductsCategories_EShopProductsCategories_ParentId",
                table: "EShopProductsCategories");

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
                name: "PK_EShopProductsProductInventories",
                table: "EShopProductsProductInventories");

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
                name: "PK_EShopPluginsBasketsProductUpdates",
                table: "EShopPluginsBasketsProductUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopPluginsBasketsBasketItems",
                table: "EShopPluginsBasketsBasketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopPaymentsRefunds",
                table: "EShopPaymentsRefunds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopPaymentsPayments",
                table: "EShopPaymentsPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopPaymentsPaymentItems",
                table: "EShopPaymentsPaymentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopOrdersOrders",
                table: "EShopOrdersOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EShopOrdersOrderLines",
                table: "EShopOrdersOrderLines");

            migrationBuilder.RenameTable(
                name: "EShopStoresStores",
                newName: "EasyAbpEShopStoresStores");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductTypes",
                newName: "EasyAbpEShopProductsProductTypes");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductStores",
                newName: "EasyAbpEShopProductsProductStores");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductSkus",
                newName: "EasyAbpEShopProductsProductSkus");

            migrationBuilder.RenameTable(
                name: "EShopProductsProducts",
                newName: "EasyAbpEShopProductsProducts");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductInventories",
                newName: "EasyAbpEShopProductsProductInventories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductHistories",
                newName: "EasyAbpEShopProductsProductHistories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductDetails",
                newName: "EasyAbpEShopProductsProductDetails");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductDetailHistories",
                newName: "EasyAbpEShopProductsProductDetailHistories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductCategories",
                newName: "EasyAbpEShopProductsProductCategories");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductAttributes",
                newName: "EasyAbpEShopProductsProductAttributes");

            migrationBuilder.RenameTable(
                name: "EShopProductsProductAttributeOptions",
                newName: "EasyAbpEShopProductsProductAttributeOptions");

            migrationBuilder.RenameTable(
                name: "EShopProductsCategories",
                newName: "EasyAbpEShopProductsCategories");

            migrationBuilder.RenameTable(
                name: "EShopPluginsBasketsProductUpdates",
                newName: "EasyAbpEShopPluginsBasketsProductUpdates");

            migrationBuilder.RenameTable(
                name: "EShopPluginsBasketsBasketItems",
                newName: "EasyAbpEShopPluginsBasketsBasketItems");

            migrationBuilder.RenameTable(
                name: "EShopPaymentsRefunds",
                newName: "EasyAbpEShopPaymentsRefunds");

            migrationBuilder.RenameTable(
                name: "EShopPaymentsPayments",
                newName: "EasyAbpEShopPaymentsPayments");

            migrationBuilder.RenameTable(
                name: "EShopPaymentsPaymentItems",
                newName: "EasyAbpEShopPaymentsPaymentItems");

            migrationBuilder.RenameTable(
                name: "EShopOrdersOrders",
                newName: "EasyAbpEShopOrdersOrders");

            migrationBuilder.RenameTable(
                name: "EShopOrdersOrderLines",
                newName: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductSkus_ProductId",
                table: "EasyAbpEShopProductsProductSkus",
                newName: "IX_EasyAbpEShopProductsProductSkus_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProducts_UniqueName",
                table: "EasyAbpEShopProductsProducts",
                newName: "IX_EasyAbpEShopProductsProducts_UniqueName");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductInventories_ProductSkuId",
                table: "EasyAbpEShopProductsProductInventories",
                newName: "IX_EasyAbpEShopProductsProductInventories_ProductSkuId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductHistories_ProductId",
                table: "EasyAbpEShopProductsProductHistories",
                newName: "IX_EasyAbpEShopProductsProductHistories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductHistories_ModificationTime",
                table: "EasyAbpEShopProductsProductHistories",
                newName: "IX_EasyAbpEShopProductsProductHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductDetailHistories_ProductDetailId",
                table: "EasyAbpEShopProductsProductDetailHistories",
                newName: "IX_EasyAbpEShopProductsProductDetailHistories_ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductDetailHistories_ModificationTime",
                table: "EasyAbpEShopProductsProductDetailHistories",
                newName: "IX_EasyAbpEShopProductsProductDetailHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductAttributes_ProductId",
                table: "EasyAbpEShopProductsProductAttributes",
                newName: "IX_EasyAbpEShopProductsProductAttributes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsProductAttributeOptions_ProductAttributeId",
                table: "EasyAbpEShopProductsProductAttributeOptions",
                newName: "IX_EasyAbpEShopProductsProductAttributeOptions_ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsCategories_UniqueName",
                table: "EasyAbpEShopProductsCategories",
                newName: "IX_EasyAbpEShopProductsCategories_UniqueName");

            migrationBuilder.RenameIndex(
                name: "IX_EShopProductsCategories_ParentId",
                table: "EasyAbpEShopProductsCategories",
                newName: "IX_EasyAbpEShopProductsCategories_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopPluginsBasketsProductUpdates_ProductSkuId",
                table: "EasyAbpEShopPluginsBasketsProductUpdates",
                newName: "IX_EasyAbpEShopPluginsBasketsProductUpdates_ProductSkuId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopPluginsBasketsBasketItems_UserId",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                newName: "IX_EasyAbpEShopPluginsBasketsBasketItems_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopPaymentsPaymentItems_PaymentId",
                table: "EasyAbpEShopPaymentsPaymentItems",
                newName: "IX_EasyAbpEShopPaymentsPaymentItems_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_EShopOrdersOrders_OrderNumber",
                table: "EasyAbpEShopOrdersOrders",
                newName: "IX_EasyAbpEShopOrdersOrders_OrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EShopOrdersOrderLines_OrderId",
                table: "EasyAbpEShopOrdersOrderLines",
                newName: "IX_EasyAbpEShopOrdersOrderLines_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopStoresStores",
                table: "EasyAbpEShopStoresStores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductTypes",
                table: "EasyAbpEShopProductsProductTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductStores",
                table: "EasyAbpEShopProductsProductStores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductSkus",
                table: "EasyAbpEShopProductsProductSkus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProducts",
                table: "EasyAbpEShopProductsProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductInventories",
                table: "EasyAbpEShopProductsProductInventories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductHistories",
                table: "EasyAbpEShopProductsProductHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductDetails",
                table: "EasyAbpEShopProductsProductDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductDetailHistories",
                table: "EasyAbpEShopProductsProductDetailHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductCategories",
                table: "EasyAbpEShopProductsProductCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductAttributes",
                table: "EasyAbpEShopProductsProductAttributes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductAttributeOptions",
                table: "EasyAbpEShopProductsProductAttributeOptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopProductsCategories",
                table: "EasyAbpEShopProductsCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPluginsBasketsProductUpdates",
                table: "EasyAbpEShopPluginsBasketsProductUpdates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPluginsBasketsBasketItems",
                table: "EasyAbpEShopPluginsBasketsBasketItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsRefunds",
                table: "EasyAbpEShopPaymentsRefunds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsPayments",
                table: "EasyAbpEShopPaymentsPayments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsPaymentItems",
                table: "EasyAbpEShopPaymentsPaymentItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopOrdersOrders",
                table: "EasyAbpEShopOrdersOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpEShopOrdersOrderLines",
                table: "EasyAbpEShopOrdersOrderLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopOrdersOrderLines_EasyAbpEShopOrdersOrders_OrderId",
                table: "EasyAbpEShopOrdersOrderLines",
                column: "OrderId",
                principalTable: "EasyAbpEShopOrdersOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopPaymentsPaymentItems_EasyAbpEShopPaymentsPayments_PaymentId",
                table: "EasyAbpEShopPaymentsPaymentItems",
                column: "PaymentId",
                principalTable: "EasyAbpEShopPaymentsPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopProductsCategories_EasyAbpEShopProductsCategories_ParentId",
                table: "EasyAbpEShopProductsCategories",
                column: "ParentId",
                principalTable: "EasyAbpEShopProductsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopProductsProductAttributeOptions_EasyAbpEShopProductsProductAttributes_ProductAttributeId",
                table: "EasyAbpEShopProductsProductAttributeOptions",
                column: "ProductAttributeId",
                principalTable: "EasyAbpEShopProductsProductAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopProductsProductAttributes_EasyAbpEShopProductsProducts_ProductId",
                table: "EasyAbpEShopProductsProductAttributes",
                column: "ProductId",
                principalTable: "EasyAbpEShopProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EasyAbpEShopProductsProductSkus_EasyAbpEShopProductsProducts_ProductId",
                table: "EasyAbpEShopProductsProductSkus",
                column: "ProductId",
                principalTable: "EasyAbpEShopProductsProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopOrdersOrderLines_EasyAbpEShopOrdersOrders_OrderId",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopPaymentsPaymentItems_EasyAbpEShopPaymentsPayments_PaymentId",
                table: "EasyAbpEShopPaymentsPaymentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopProductsCategories_EasyAbpEShopProductsCategories_ParentId",
                table: "EasyAbpEShopProductsCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopProductsProductAttributeOptions_EasyAbpEShopProductsProductAttributes_ProductAttributeId",
                table: "EasyAbpEShopProductsProductAttributeOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopProductsProductAttributes_EasyAbpEShopProductsProducts_ProductId",
                table: "EasyAbpEShopProductsProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_EasyAbpEShopProductsProductSkus_EasyAbpEShopProductsProducts_ProductId",
                table: "EasyAbpEShopProductsProductSkus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopStoresStores",
                table: "EasyAbpEShopStoresStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductTypes",
                table: "EasyAbpEShopProductsProductTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductStores",
                table: "EasyAbpEShopProductsProductStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductSkus",
                table: "EasyAbpEShopProductsProductSkus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProducts",
                table: "EasyAbpEShopProductsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductInventories",
                table: "EasyAbpEShopProductsProductInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductHistories",
                table: "EasyAbpEShopProductsProductHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductDetails",
                table: "EasyAbpEShopProductsProductDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductDetailHistories",
                table: "EasyAbpEShopProductsProductDetailHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductCategories",
                table: "EasyAbpEShopProductsProductCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductAttributes",
                table: "EasyAbpEShopProductsProductAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsProductAttributeOptions",
                table: "EasyAbpEShopProductsProductAttributeOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopProductsCategories",
                table: "EasyAbpEShopProductsCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPluginsBasketsProductUpdates",
                table: "EasyAbpEShopPluginsBasketsProductUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPluginsBasketsBasketItems",
                table: "EasyAbpEShopPluginsBasketsBasketItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsRefunds",
                table: "EasyAbpEShopPaymentsRefunds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsPayments",
                table: "EasyAbpEShopPaymentsPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopPaymentsPaymentItems",
                table: "EasyAbpEShopPaymentsPaymentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopOrdersOrders",
                table: "EasyAbpEShopOrdersOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpEShopOrdersOrderLines",
                table: "EasyAbpEShopOrdersOrderLines");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopStoresStores",
                newName: "EShopStoresStores");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductTypes",
                newName: "EShopProductsProductTypes");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductStores",
                newName: "EShopProductsProductStores");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductSkus",
                newName: "EShopProductsProductSkus");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProducts",
                newName: "EShopProductsProducts");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductInventories",
                newName: "EShopProductsProductInventories");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductHistories",
                newName: "EShopProductsProductHistories");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductDetails",
                newName: "EShopProductsProductDetails");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductDetailHistories",
                newName: "EShopProductsProductDetailHistories");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductCategories",
                newName: "EShopProductsProductCategories");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductAttributes",
                newName: "EShopProductsProductAttributes");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsProductAttributeOptions",
                newName: "EShopProductsProductAttributeOptions");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopProductsCategories",
                newName: "EShopProductsCategories");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPluginsBasketsProductUpdates",
                newName: "EShopPluginsBasketsProductUpdates");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPluginsBasketsBasketItems",
                newName: "EShopPluginsBasketsBasketItems");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPaymentsRefunds",
                newName: "EShopPaymentsRefunds");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPaymentsPayments",
                newName: "EShopPaymentsPayments");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopPaymentsPaymentItems",
                newName: "EShopPaymentsPaymentItems");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopOrdersOrders",
                newName: "EShopOrdersOrders");

            migrationBuilder.RenameTable(
                name: "EasyAbpEShopOrdersOrderLines",
                newName: "EShopOrdersOrderLines");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductSkus_ProductId",
                table: "EShopProductsProductSkus",
                newName: "IX_EShopProductsProductSkus_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProducts_UniqueName",
                table: "EShopProductsProducts",
                newName: "IX_EShopProductsProducts_UniqueName");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductInventories_ProductSkuId",
                table: "EShopProductsProductInventories",
                newName: "IX_EShopProductsProductInventories_ProductSkuId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductHistories_ProductId",
                table: "EShopProductsProductHistories",
                newName: "IX_EShopProductsProductHistories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductHistories_ModificationTime",
                table: "EShopProductsProductHistories",
                newName: "IX_EShopProductsProductHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductDetailHistories_ProductDetailId",
                table: "EShopProductsProductDetailHistories",
                newName: "IX_EShopProductsProductDetailHistories_ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductDetailHistories_ModificationTime",
                table: "EShopProductsProductDetailHistories",
                newName: "IX_EShopProductsProductDetailHistories_ModificationTime");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductAttributes_ProductId",
                table: "EShopProductsProductAttributes",
                newName: "IX_EShopProductsProductAttributes_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsProductAttributeOptions_ProductAttributeId",
                table: "EShopProductsProductAttributeOptions",
                newName: "IX_EShopProductsProductAttributeOptions_ProductAttributeId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsCategories_UniqueName",
                table: "EShopProductsCategories",
                newName: "IX_EShopProductsCategories_UniqueName");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopProductsCategories_ParentId",
                table: "EShopProductsCategories",
                newName: "IX_EShopProductsCategories_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopPluginsBasketsProductUpdates_ProductSkuId",
                table: "EShopPluginsBasketsProductUpdates",
                newName: "IX_EShopPluginsBasketsProductUpdates_ProductSkuId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopPluginsBasketsBasketItems_UserId",
                table: "EShopPluginsBasketsBasketItems",
                newName: "IX_EShopPluginsBasketsBasketItems_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopPaymentsPaymentItems_PaymentId",
                table: "EShopPaymentsPaymentItems",
                newName: "IX_EShopPaymentsPaymentItems_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopOrdersOrders_OrderNumber",
                table: "EShopOrdersOrders",
                newName: "IX_EShopOrdersOrders_OrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_EasyAbpEShopOrdersOrderLines_OrderId",
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
                name: "PK_EShopProductsProductInventories",
                table: "EShopProductsProductInventories",
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
                name: "PK_EShopPluginsBasketsProductUpdates",
                table: "EShopPluginsBasketsProductUpdates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopPluginsBasketsBasketItems",
                table: "EShopPluginsBasketsBasketItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopPaymentsRefunds",
                table: "EShopPaymentsRefunds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopPaymentsPayments",
                table: "EShopPaymentsPayments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EShopPaymentsPaymentItems",
                table: "EShopPaymentsPaymentItems",
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
                name: "FK_EShopPaymentsPaymentItems_EShopPaymentsPayments_PaymentId",
                table: "EShopPaymentsPaymentItems",
                column: "PaymentId",
                principalTable: "EShopPaymentsPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EShopProductsCategories_EShopProductsCategories_ParentId",
                table: "EShopProductsCategories",
                column: "ParentId",
                principalTable: "EShopProductsCategories",
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
    }
}
