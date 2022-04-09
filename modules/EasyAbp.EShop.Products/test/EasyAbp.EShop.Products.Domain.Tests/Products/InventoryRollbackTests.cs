using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.Products
{
    public class InventoryRollbackTests : ProductsDomainTestBase
    {
        private InventoryRollbackOrderCanceledEventHandler EventHandler { get; }
        private IProductRepository ProductRepository { get; }
        private IProductManager ProductManager { get; }
        
        public InventoryRollbackTests()
        {
            EventHandler = ServiceProvider.GetRequiredService<InventoryRollbackOrderCanceledEventHandler>();
            ProductRepository = ServiceProvider.GetRequiredService<IProductRepository>();
            ProductManager = ServiceProvider.GetRequiredService<IProductManager>();
        }
        
        [Fact]
        public async Task Should_Roll_Back_ReduceAfterPlacing_Inventory_If_Order_Is_Not_Paid()
        {
            await TestAsync(InventoryStrategy.ReduceAfterPlacing, false, true);
        }

        [Fact]
        public async Task Should_Not_Roll_Back_ReduceAfterPlacing_Inventory_If_Order_Is_Paid()
        {
            await TestAsync(InventoryStrategy.ReduceAfterPlacing, true, false);
        }

        [Fact]
        public async Task Should_Not_Roll_Back_ReduceAfterPayment_Inventory_If_Order_Is_Not_Paid()
        {
            await TestAsync(InventoryStrategy.ReduceAfterPayment, false, false);
        }

        [Fact]
        public async Task Should_Not_Roll_Back_ReduceAfterPayment_Inventory_If_Order_Is_Paid()
        {
            await TestAsync(InventoryStrategy.ReduceAfterPayment, true, false);
        }

        [Fact]
        public async Task Should_Not_Roll_Back_NoNeed_Inventory_If_Order_Is_Not_Paid()
        {
            await TestAsync(InventoryStrategy.NoNeed, false, false);
        }

        [Fact]
        public async Task Should_Not_Roll_Back_NoNeed_Inventory_If_Order_Is_Paid()
        {
            await TestAsync(InventoryStrategy.NoNeed, true, false);
        }

        protected async Task TestAsync(InventoryStrategy inventoryStrategy, bool orderPaid, bool expectRollback)
        {
            var product = await ProductRepository.GetAsync(ProductsTestData.Product1Id);
            var productSku = product.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

            typeof(Product).GetProperty("InventoryStrategy")!.SetValue(product, inventoryStrategy);
            
            await ProductRepository.UpdateAsync(product, true);

            (await ProductManager.TryIncreaseInventoryAsync(product, productSku, 3, false)).ShouldBeTrue();
            (await ProductManager.TryReduceInventoryAsync(product, productSku, 3, true)).ShouldBeTrue();

            var inventoryDataModel = await ProductManager.GetInventoryDataAsync(product, productSku);
            
            inventoryDataModel.Inventory.ShouldBe(0);

            await EventHandler.HandleEventAsync(new OrderCanceledEto(
                new OrderEto
                {
                    #region properties

                    Id = Guid.NewGuid(),
                    TenantId = null,
                    StoreId = ProductsTestData.Store1Id,
                    OrderNumber = null,
                    CustomerUserId = Guid.NewGuid(),
                    OrderStatus = OrderStatus.Processing,
                    Currency = null,
                    ProductTotalPrice = 0,
                    TotalDiscount = 0,
                    TotalPrice = 0,
                    ActualTotalPrice = 0,
                    RefundAmount = 0,
                    CustomerRemark = null,
                    StaffRemark = null,
                    PaymentId = orderPaid ? Guid.NewGuid() : null,
                    PaidTime = orderPaid ? DateTime.Now : null,
                    CompletionTime = null,
                    CanceledTime = null,
                    CancellationReason = null,
                    ReducedInventoryAfterPlacingTime = null,
                    ReducedInventoryAfterPaymentTime = null,
                    PaymentExpiration = null,
                    OrderLines = new List<OrderLineEto>
                    {
                        new()
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            ProductSkuId = productSku.Id,
                            ProductModificationTime = default,
                            ProductDetailModificationTime = default,
                            ProductGroupName = null,
                            ProductGroupDisplayName = null,
                            ProductUniqueName = null,
                            ProductDisplayName = null,
                            SkuName = null,
                            SkuDescription = null,
                            MediaResources = null,
                            Currency = null,
                            UnitPrice = 0,
                            TotalPrice = 0,
                            TotalDiscount = 0,
                            ActualTotalPrice = 0,
                            Quantity = 3,
                            RefundedQuantity = 0,
                            RefundAmount = 0
                        }
                    },
                    CreationTime = default,
                    CreatorId = null,
                    LastModificationTime = null,
                    LastModifierId = null,
                    IsDeleted = false,
                    DeletionTime = null,
                    DeleterId = null

                    #endregion
                }
            ));

            product = await ProductRepository.GetAsync(ProductsTestData.Product1Id);
            productSku = product.ProductSkus.Single(x => x.Id == ProductsTestData.Product1Sku1Id);

            inventoryDataModel = await ProductManager.GetInventoryDataAsync(product, productSku);
            
            inventoryDataModel.Inventory.ShouldBe(expectRollback ? 3 : 0);
        }
    }
}
