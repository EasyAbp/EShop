using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class FlashSaleResult : FullAuditedAggregateRoot<Guid>, IFlashSaleResult, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

    public virtual Guid PlanId { get; protected set; }

    public virtual FlashSaleResultStatus Status { get; protected set; }

    public virtual string Reason { get; protected set; }

    public virtual Guid UserId { get; protected set; }

    public virtual Guid? OrderId { get; protected set; }

    public virtual DateTime ReducedInventoryTime { get; protected set; }

    protected FlashSaleResult() { }

    public FlashSaleResult(
        Guid id, Guid? tenantId, Guid storeId, Guid planId, Guid userId, DateTime reducedInventoryTime) : base(id)
    {
        TenantId = tenantId;
        StoreId = storeId;
        PlanId = planId;
        Status = FlashSaleResultStatus.Pending;
        UserId = userId;
        ReducedInventoryTime = reducedInventoryTime;
    }

    public void MarkAsSuccessful(Guid orderId)
    {
        if (Status != FlashSaleResultStatus.Pending)
        {
            throw new FlashSaleResultStatusNotPendingException(Id);
        }
        Status = FlashSaleResultStatus.Successful;
        OrderId = orderId;
    }

    public void MarkAsFailed([NotNull] string reason)
    {
        Status = FlashSaleResultStatus.Failed;
        Reason = Check.NotNullOrEmpty(reason, nameof(reason));
    }
}
