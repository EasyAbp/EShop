using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class OrdersPluginsFlashSalesApplicationTestBase : FlashSalesTestBase<EShopOrdersPluginsFlashSalesApplicationTestsModule>
{
    protected virtual ProductDto CreateMockProductDto()
    {
        return new ProductDto
        {
            CreationTime = DateTime.Now,
            IsPublished = true,
            Id = FlashSalesTestData.Product1Id,
            StoreId = FlashSalesTestData.Store1Id,
            ProductGroupName = "Default",
            ProductGroupDisplayName = "Default",
            UniqueName = "Pencil",
            DisplayName = "Hello pencil",
            ProductDetailId = FlashSalesTestData.ProductDetail1Id,
            ProductSkus = new List<ProductSkuDto>
                {
                    new ProductSkuDto
                    {
                        Id = FlashSalesTestData.ProductSku1Id,
                        Name = "My SKU",
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>(),
                        Price = 1m,
                        Currency = "USD",
                        ProductDetailId = null,
                        Inventory = 10,
                    },
                    new ProductSkuDto
                    {
                        Id = FlashSalesTestData.ProductSku2Id,
                        Name = "My SKU 2",
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>(),
                        Price = 2m,
                        Currency = "USD",
                        ProductDetailId = FlashSalesTestData.ProductDetail2Id,
                        Inventory = 0
                    },
                    new ProductSkuDto
                    {
                        Id = FlashSalesTestData.ProductSku3Id,
                        Name = "My SKU 3",
                        OrderMinQuantity = 0,
                        OrderMaxQuantity = 100,
                        AttributeOptionIds = new List<Guid>(),
                        Price = 3m,
                        Currency = "USD",
                        ProductDetailId = FlashSalesTestData.ProductDetail2Id,
                        Inventory = 1
                    }
                },
            InventoryStrategy = InventoryStrategy.FlashSales,
            LastModificationTime = FlashSalesTestData.ProductLastModificationTime
        };
    }
}
