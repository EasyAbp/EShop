using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesPlanManager : DomainService
{
    public virtual Task<FlashSalesPlan> CreateAsync(
        Guid storeId,
        DateTime beginTime,
        DateTime endTime,
        Guid productId,
        Guid productSkuId,
        bool isActive)
    {
        var flashSalesPlan = new FlashSalesPlan(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            storeId,
            beginTime,
            endTime,
            productId,
            productSkuId,
            isActive
        );
        return Task.FromResult(flashSalesPlan);
    }

    public virtual Task<FlashSalesPlan> UpdateAsync(
        FlashSalesPlan flashSalesPlan,
        DateTime beginTime,
        DateTime endTime,
        Guid productId,
        Guid productSkuId,
        bool isPublished)
    {
        flashSalesPlan.SetTimeRange(beginTime, endTime);
        flashSalesPlan.SetProduct(productId, productSkuId);
        flashSalesPlan.SetPublished(isPublished);

        return Task.FromResult(flashSalesPlan);
    }
}
