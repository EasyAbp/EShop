using System.Threading.Tasks;
using System;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Volo.Abp;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Users;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Products;
using System.Collections.Generic;

namespace EasyAbp.EShop.Plugins.FlashSales;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class FlashSalesApplicationTestBase : FlashSalesTestBase<EShopPluginsFlashSalesApplicationTestModule>
{
    protected IGuidGenerator GuidGenerator { get; }

    protected ICurrentTenant CurrentTenant { get; }

    protected IClock Clock { get; }

    protected ICurrentUser CurrentUser { get; }

    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    protected IFlashSalePlanRepository FlashSalePlanRepository { get; }

    protected FlashSalesApplicationTestBase()
    {
        GuidGenerator = GetRequiredService<IGuidGenerator>();
        CurrentTenant = GetRequiredService<ICurrentTenant>();
        Clock = GetRequiredService<IClock>();
        CurrentUser = GetRequiredService<ICurrentUser>();
        FlashSalePlanRepository = GetRequiredService<IFlashSalePlanRepository>();
        FlashSaleResultRepository = GetRequiredService<IFlashSaleResultRepository>();
    }

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

    protected virtual async Task<FlashSaleResult> CreatePendingResultAsync(Guid planId, Guid storeId, Guid userId)
    {
        return await WithUnitOfWorkAsync(async () => await FlashSaleResultRepository.InsertAsync(
            new FlashSaleResult(GuidGenerator.Create(), CurrentTenant.Id, storeId, planId, userId, DateTime.Now)
        ));
    }

    protected virtual async Task<FlashSalePlan> CreateFlashSalePlanAsync(bool useSku2 = false, CreateTimeRange timeRange = CreateTimeRange.Starting, bool isPublished = true)
    {
        DateTime beginTime;
        DateTime endTime;

        switch (timeRange)
        {
            case CreateTimeRange.Starting:
                beginTime = Clock.Now;
                endTime = beginTime.AddMinutes(30);
                break;
            case CreateTimeRange.NotStart:
                beginTime = Clock.Now.AddMinutes(10);
                endTime = beginTime.AddMinutes(30);
                break;
            case CreateTimeRange.Expired:
                beginTime = Clock.Now.AddDays(-1);
                endTime = beginTime.AddMinutes(30);
                break;
            case CreateTimeRange.WillBeExpired:
                beginTime = Clock.Now.AddDays(-30);
                endTime = Clock.Now.AddSeconds(1);
                break;
            default:
                throw new AbpException();
        }

        var flashSalePlan = new FlashSalePlan(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            FlashSalesTestData.Store1Id,
            beginTime,
            endTime,
            FlashSalesTestData.Product1Id,
            useSku2 ? FlashSalesTestData.ProductSku2Id : FlashSalesTestData.ProductSku1Id,
            isPublished
        );
        return await WithUnitOfWorkAsync(async () => await FlashSalePlanRepository.InsertAsync(flashSalePlan, autoSave: true));
    }
}
