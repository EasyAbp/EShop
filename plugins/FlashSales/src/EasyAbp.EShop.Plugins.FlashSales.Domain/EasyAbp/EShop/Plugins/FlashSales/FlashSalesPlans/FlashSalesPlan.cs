using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class FlashSalesPlan : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMultiStore
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

    public virtual DateTime BeginTime { get; set; }

    public virtual DateTime EndTime { get; set; }

    public virtual Guid ProductId { get; protected set; }

    public virtual Guid ProductSkuId { get; protected set; }

    public virtual bool IsPublished { get; protected set; }

    protected FlashSalesPlan()
    {
    }

    public FlashSalesPlan(Guid id, Guid? tenantId, Guid storeId, DateTime beginTime, DateTime endTime, Guid productId, Guid productSkuId, bool isPublished)
        : base(id)
    {
        TenantId = tenantId;
        SetTimeRange(beginTime, endTime);
        SetProductSku(storeId, productId, productSkuId);
        SetPublished(isPublished);
    }

    public void SetTimeRange(DateTime beginTime, DateTime endTime)
    {
        if (beginTime > endTime)
        {
            throw new InvalidEndTimeException();
        }

        BeginTime = beginTime;
        EndTime = endTime;
    }

    public void SetProductSku(Guid storeId, Guid productId, Guid productSkuId)
    {
        StoreId = storeId;
        ProductId = productId;
        ProductSkuId = productSkuId;
    }

    public void SetPublished(bool isPublished)
    {
        IsPublished = isPublished;
    }
}
