using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSalePlanTests
{
    [Fact]
    public void Should_Throw_InvalidEndTimeException()
    {
        Assert.Throws<InvalidEndTimeException>(() =>
        {
            new FlashSalePlan(
                id: Guid.NewGuid(),
                tenantId: null,
                storeId: Guid.NewGuid(),
                beginTime: DateTime.Now,
                endTime: DateTime.Now.AddSeconds(-1),
                productId: Guid.NewGuid(),
                productSkuId: Guid.NewGuid(),
                isPublished: true
            );
        });
    }

    [Fact]
    public void SetTimeRange()
    {
        var existPlan = new FlashSalePlan(
                id: Guid.NewGuid(),
                tenantId: null,
                storeId: Guid.NewGuid(),
                beginTime: DateTime.Now,
                endTime: DateTime.Now.AddSeconds(1),
                productId: Guid.NewGuid(),
                productSkuId: Guid.NewGuid(),
                isPublished: true
            );

        var newBeginTime = DateTime.Now;
        var newEndTime = newBeginTime.AddMinutes(1);

        existPlan.SetTimeRange(newBeginTime, newEndTime);

        existPlan.BeginTime.ShouldBe(newBeginTime);
        existPlan.EndTime.ShouldBe(newEndTime);
    }

    [Fact]
    public void SetTimeRange_Should_Throw_InvalidEndTimeException_When_Set_InvalidEndTime()
    {
        var existPlan = new FlashSalePlan(
                id: Guid.NewGuid(),
                tenantId: null,
                storeId: Guid.NewGuid(),
                beginTime: DateTime.Now,
                endTime: DateTime.Now.AddSeconds(1),
                productId: Guid.NewGuid(),
                productSkuId: Guid.NewGuid(),
                isPublished: true
            );

        Assert.Throws<InvalidEndTimeException>(() => existPlan.SetTimeRange(DateTime.Now, DateTime.Now.AddMinutes(-1)));
    }

    [Fact]
    public void SetProductSku()
    {
        var existPlan = new FlashSalePlan(
                id: Guid.NewGuid(),
                tenantId: null,
                storeId: Guid.NewGuid(),
                beginTime: DateTime.Now,
                endTime: DateTime.Now.AddSeconds(1),
                productId: Guid.NewGuid(),
                productSkuId: Guid.NewGuid(),
                isPublished: true
            );

        var newStoreId = Guid.NewGuid();
        var newProductId = Guid.NewGuid();
        var newProductSkuId = Guid.NewGuid();

        existPlan.SetProductSku(newStoreId, newProductId, newProductSkuId);

        existPlan.StoreId.ShouldBe(newStoreId);
        existPlan.ProductId.ShouldBe(newProductId);
        existPlan.ProductSkuId.ShouldBe(newProductSkuId);
    }

    [Fact]
    public void SetPublished()
    {
        var existPlan = new FlashSalePlan(
                id: Guid.NewGuid(),
                tenantId: null,
                storeId: Guid.NewGuid(),
                beginTime: DateTime.Now,
                endTime: DateTime.Now.AddSeconds(1),
                productId: Guid.NewGuid(),
                productSkuId: Guid.NewGuid(),
                isPublished: true
            );

        existPlan.SetPublished(false);

        existPlan.IsPublished.ShouldBe(false);
    }
}
